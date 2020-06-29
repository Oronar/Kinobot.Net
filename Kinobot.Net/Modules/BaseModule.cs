using Discord.Commands;
using Kinobot.Net.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	public abstract class BaseModule : ModuleBase<SocketCommandContext>
	{
		internal readonly ILoggingService loggingService;

		public BaseModule(ILoggingService loggingService)
		{
			this.loggingService = loggingService;
		}

		public async Task ExecuteAsync(Func<Task> command)
		{
			if (command == null)
			{
				throw new ArgumentNullException($"{nameof(command)} cannot be null.");
			}

			try
			{
				await command.Invoke();
			}
			catch (Exception e)
			{
				await loggingService.LogAsync(e.ToString());
				await ReplyAsync($"Error encountered. Please report this issue.");

				throw;
			}
		}

		internal async Task DotLeaderReplyAsync(IDictionary<string, string> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException(nameof(dictionary));
			}

			var longestKeyLength = dictionary.Keys.Max(k => k.Length);

			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("```");
			foreach (var kvp in dictionary)
			{
				stringBuilder.Append(kvp.Key.PadRight(longestKeyLength + 5, '.'));
				stringBuilder.AppendLine(kvp.Value);
			}
			stringBuilder.AppendLine("```");

			await ReplyAsync(stringBuilder.ToString());
		}
	}
}