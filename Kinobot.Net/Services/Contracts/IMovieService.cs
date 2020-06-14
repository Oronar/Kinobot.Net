using Kinobot.Net.Models;
using System.Threading.Tasks;

namespace Kinobot.Net.Services.Contracts
{
	public interface IMovieService
	{
		Task<Movie> GetAsync(int id);

		Task<Movie> GetAsync(string query);
	}
}