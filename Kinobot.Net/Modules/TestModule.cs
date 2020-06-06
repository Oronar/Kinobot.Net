using Discord.Commands;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	public class TestModule : ModuleBase<SocketCommandContext>
	{
		[Command("test")]
		[Summary("Test command")]
		public async Task TestAsync()
		{
			await ReplyAsync(Properties.Resources.testResponse);
		}
	}
}