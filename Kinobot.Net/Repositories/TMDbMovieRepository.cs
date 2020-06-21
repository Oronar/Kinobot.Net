using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using System;
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
			this.tmdbClient = tmdbClient ?? throw new ArgumentNullException($"{nameof(tmdbClient)}");
			this.mapper = mapper;
			this.tmdbClient.GetConfigAsync();
		}

		public async Task<Movie> GetAsync(int id)
		{
			var result = await tmdbClient.GetMovieAsync(id, MovieMethods.Images | MovieMethods.Credits);

			if (result == null)
			{
				throw new KeyNotFoundException($"TMDB ID {id} not found.");
			}

			var movie = mapper.Map<Movie>(result);

			if (result.Images.Posters.Any())
			{
				var uri = tmdbClient.GetImageUrl("original", result.Images.Posters.First().FilePath);
				movie.ImageUri = uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.InvariantCultureIgnoreCase)
					? uri
					: new Uri(uri.ToString().Replace("http", "https", StringComparison.InvariantCultureIgnoreCase)); // TODO: Move into AutoMapper ValueResolver
			}

			return movie;
		}

		public async Task<Movie> GetAsync(string name)
		{
			var searchContainer = await tmdbClient.SearchMovieAsync(name);

			if (!searchContainer.Results.Any())
			{
				throw new KeyNotFoundException($"No results for '{name}'");
			}

			return await GetAsync(searchContainer.Results.First().Id);
		}

		public async Task<SearchPage<Movie>> SearchAsync(string query, int page = 1)
		{
			var searchContainer = await tmdbClient.SearchMovieAsync(query, page);
			return mapper.Map<SearchPage<Movie>>(searchContainer);
		}
	}
}