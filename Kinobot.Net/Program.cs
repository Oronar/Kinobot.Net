using System.Threading.Tasks;

namespace Kinobot.Net
{
	public static class Program
	{
		public async static Task Main()
		{
			await new Startup().RunAsync();
		}
	}
}