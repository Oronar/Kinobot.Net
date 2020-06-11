using Discord.Commands;
using Kinobot.Net.Services.Contracts;
using System;
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
	}
}