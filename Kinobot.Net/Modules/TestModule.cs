using Discord.Commands;
using Kinobot.Net.Modules.Contracts;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	public class TestModule : ModuleBase<SocketCommandContext>, ITestModule
	{
		[Command("test")]
		[Summary("Test command")]
		public async Task TestAsync()
		{
			await ReplyAsync("Test successful, bot is functioning.");
		}
	}
}