using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;

namespace Kinobot.Net.Repositories
{
	public class TMDbMovieRepository : IMovieRepository
	{
		private readonly TMDbClient tmdbClient;
		private readonly IMapper mapper;

		public TMDbMovieRepository(TMDbClient client, IMapper mapper)
		{
			tmdbClient = client;
			this.mapper = mapper;
		}

		public async Task<Movie> GetAsync(int id)
		{
			var movie = mapper.Map<Movie>(await tmdbClient.GetMovieAsync(id));

			if (movie == null)
			{
				throw new KeyNotFoundException($"TMDB ID {id} not found.");
			}

			return movie;
		}

		public async Task<IEnumerable<Movie>> SearchAsync(string query)
		{
			var searchContainer = await tmdbClient.SearchMovieAsync(query);
			return searchContainer.Results.Select(m => mapper.Map<Movie>(m));
		}
	}
}