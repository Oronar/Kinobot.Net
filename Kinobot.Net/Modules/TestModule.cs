using Discord.Commands;
using Kinobot.Net.Services.Contracts;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	public class TestModule : BaseModule
	{
		public TestModule(ILoggingService loggingService) : base(loggingService)
		{
		}

		[Command("test")]
		[Summary("Test command")]
		public async Task TestAsync()
		{
			await ExecuteAsync(async () =>
			{
				await ReplyAsync("Test successful, bot is functioning.");
			});
		}
	}
}