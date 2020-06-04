using Kinobot.Net.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinobot.Net.Repositories.Contracts
{
	public interface IMovieRepository
	{
		Task<Movie> GetAsync(int id);

		Task<IEnumerable<Movie>> SearchAsync(string query, int page = 0);
	}
}