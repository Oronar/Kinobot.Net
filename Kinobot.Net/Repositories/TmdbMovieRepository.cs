using AutoMapper;
using Kinobot.Net.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using Movie = Kinobot.Net.Models.Movie;

namespace Kinobot.Net.Repositories
{
	public class TMDbMovieRepository : IMovieRepository
	{
		private readonly TMDbClient tmdbClient;
		private readonly IMapper mapper;

		public TMDbMovieRepository(TMDbClient tmdbClient, IMapper mapper)
		{
			this.tmdbClient = tmdbClient;
			this.mapper = mapper;

			this.tmdbClient.GetConfigAsync();
		}

		public async Task<Movie> GetAsync(int id)
		{
			var result = await tmdbClient.GetMovieAsync(id, MovieMethods.Images);

			if (result == null)
			{
				throw new KeyNotFoundException($"TMDB ID {id} not found.");
			}

			var movie = mapper.Map<Movie>(result);

			movie.ImageUrl = GetImageUrl(result.Images.Posters.First().FilePath); // TODO: Move into AutoMapper ValueResolver

			return movie;
		}

		public async Task<IEnumerable<Movie>> SearchAsync(string query, int page = 0)
		{
			var searchContainer = await tmdbClient.SearchMovieAsync(query, page);
			return searchContainer.Results.Select(m => mapper.Map<Movie>(m));
		}

		public string GetImageUrl(string filePath, string size = "original")
		{
			return tmdbClient.GetImageUrl(size, filePath).ToString();
		}
	}
}