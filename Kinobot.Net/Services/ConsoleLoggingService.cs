﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kinobot.Net.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Kinobot.Net.Services
{
	public class ConsoleLoggingService : ILoggingService
	{
		private readonly DiscordSocketClient SocketClient;
		private readonly CommandService CommandService;

		public ConsoleLoggingService(DiscordSocketClient socketClient, CommandService commandService)
		{
			SocketClient = socketClient ?? throw new ArgumentNullException($"{nameof(socketClient)}");
			CommandService = commandService ?? throw new ArgumentNullException($"{nameof(commandService)}");

			SocketClient.Log += LogAsync;
			CommandService.Log += LogAsync;
		}

		public Task LogAsync(LogMessage message)
		{
			Console.WriteLine(message.ToString());
			return Task.CompletedTask;
		}

		public Task LogAsync(string message)
		{
			Console.WriteLine(message);
			return Task.CompletedTask;
		}
	}
}