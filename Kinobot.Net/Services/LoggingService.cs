using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kinobot.Net.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Kinobot.Net.Services
{
	public class LoggingService : ILoggingService
	{
		private readonly DiscordSocketClient SocketClient;
		private readonly CommandService CommandService;

		public LoggingService(DiscordSocketClient socketClient, CommandService commandService)
		{
			SocketClient = socketClient;
			CommandService = commandService;

			SocketClient.Log += LogAsync;
			CommandService.Log += LogAsync;
		}

		public Task LogAsync(LogMessage message)
		{
			Console.WriteLine(message.ToString());
			return Task.CompletedTask;
		}
	}
}