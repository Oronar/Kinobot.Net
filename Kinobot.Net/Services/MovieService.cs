using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<Movie> GetAsync(string title)
		{
			return await movieRepository.GetAsync(title);
		}

		public async Task<IEnumerable<Movie>> SearchAsync(string title, int amount)
		{
			var pageCount = 1;
			var results = new List<Movie>();
			do
			{
				var searchPage = await movieRepository.SearchAsync(title, pageCount);
				results.AddRange(searchPage.Results.Take(amount - results.Count));

				if (searchPage.Page >= searchPage.PageTotal)
				{
					break;
				}
				
				pageCount++;
			}
			while (results.Count < amount);

			return results;
		}
	}
}