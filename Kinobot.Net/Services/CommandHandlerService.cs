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
		}

		private async Task HandleCommandAsync(SocketMessage messageParam)
		{
			if (!(messageParam is SocketUserMessage message))
			{
				return;
			}

			int arumentIndex = 0;

			if (!message.HasMentionPrefix(SocketClient.CurrentUser, ref arumentIndex) || message.Author.IsBot)
			{
				return;
			}

			var context = new SocketCommandContext(SocketClient, message);

			await CommandService.ExecuteAsync(context: context, argPos: arumentIndex, services: ServiceProvider);
		}
	}
}