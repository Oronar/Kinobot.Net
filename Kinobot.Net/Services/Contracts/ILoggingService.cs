using Discord;
using System.Threading.Tasks;

namespace Kinobot.Net.Services.Contracts
{
	public interface ILoggingService
	{
		Task LogAsync(LogMessage message);

		Task LogStringAsync(string message);
	}
}