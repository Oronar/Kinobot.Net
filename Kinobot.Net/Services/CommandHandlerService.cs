using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kinobot.Net.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Kinobot.Net.Services
{
	public class CommandHandlerService : ICommandHandlerService
	{
		private readonly IServiceProvider ServiceProvider;
		private readonly DiscordSocketClient SocketClient;
		private readonly CommandService CommandService;

		public CommandHandlerService(IServiceProvider serviceProvider, DiscordSocketClient socketClient, CommandService commandService)
		{
			ServiceProvider = serviceProvider;
			CommandService = commandService;
			SocketClient = socketClient;
		}

		public void InjectCommandHandler()
		{
			SocketClient.MessageReceived += HandleCommandAsync;
			CommandService.CommandExecuted += OnCommandExecutedAsync;
		}

		private async Task HandleCommandAsync(SocketMessage messageParam)
		{
			if (!(messageParam is SocketUserMessage message))
			{
				return;
			}

			int argumentIndex = 0;

			if (!message.HasMentionPrefix(SocketClient.CurrentUser, ref argumentIndex) || message.Author.IsBot)
			{
				return;
			}

			var context = new SocketCommandContext(SocketClient, message);

			await CommandService.ExecuteAsync(context: context, argPos: argumentIndex, services: ServiceProvider);
		}

		private async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
		{
			if (!string.IsNullOrEmpty(result?.ErrorReason))
			{
				if (result.ErrorReason.Equals("Unknown command.", StringComparison.OrdinalIgnoreCase)) // If command is unknown, pass as a query to search command
				{
					int argumentIndex = 0;
					context.Message.HasMentionPrefix(SocketClient.CurrentUser, ref argumentIndex);

					var modifiedCommand = $"search \"{context.Message.Content.Substring(argumentIndex)}\"";
					await CommandService.ExecuteAsync(context: context, input: modifiedCommand, services: ServiceProvider);
				}
			}
		}
	}
}