using System.Threading.Tasks;

namespace Kinobot.Net.Modules.Contracts
{
	public interface IMovieModule
	{
		Task GetMovieAsync(int id);

		Task SearchMovieAsync(params string[] query);
	}
}