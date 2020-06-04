using Kinobot.Net.Models;
using System.Threading.Tasks;

namespace Kinobot.Net.Repositories.Contracts
{
	public interface IMovieRepository
	{
		Task<Movie> GetAsync(int id);
	}
}