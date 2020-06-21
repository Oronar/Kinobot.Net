using Kinobot.Net.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinobot.Net.Services.Contracts
{
	public interface IMovieService
	{
		Task<Movie> GetAsync(int id);

		Task<Movie> GetAsync(string query);

		Task<IEnumerable<Movie>> SearchAsync(string title, int amount);
	}
}