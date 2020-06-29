using Kinobot.Net.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinobot.Net.Services.Contracts
{
	public interface ITVService
	{
		Task<TVShow> GetAsync(int id);

		Task<TVShow> GetAsync(string query);

		Task<IEnumerable<TVShow>> SearchAsync(string title, int amount);
	}
}