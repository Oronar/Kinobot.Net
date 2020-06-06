using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services.Contracts;
using System.Threading.Tasks;

namespace Kinobot.Net.Services
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository movieRepository;

		public MovieService(IMovieRepository movieRepository)
		{
			this.movieRepository = movieRepository;
		}

		public async Task<Movie> GetAsync(int id)
		{
			return await movieRepository.GetAsync(id);
		}

		public async Task<Movie> SearchAsync(string query)
		{
			return await movieRepository.GetAsync(query);
		}
	}
}