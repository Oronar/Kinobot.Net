using Kinobot.Net.Models;
using System.Threading.Tasks;

namespace Kinobot.Net.Repositories.Contracts
{
	public interface ITVRepository
	{
		Task<TVShow> GetAsync(int id);

		Task<TVShow> GetAsync(string name);
	}
}