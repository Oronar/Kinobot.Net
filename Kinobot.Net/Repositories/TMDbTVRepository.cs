using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.TvShows;

namespace Kinobot.Net.Repositories
{
	public class TMDbTVRepository : ITVRepository
	{
		private readonly TMDbClient tmdbClient;
		private readonly IMapper mapper;

		public TMDbTVRepository(TMDbClient tmdbClient, IMapper mapper)
		{
			this.tmdbClient = tmdbClient ?? throw new ArgumentNullException($"{nameof(tmdbClient)}");
			this.mapper = mapper;
			this.tmdbClient.GetConfigAsync();
		}

		public async Task<TVShow> GetAsync(int id)
		{
			var result = await tmdbClient.GetTvShowAsync(id, TvShowMethods.Images | TvShowMethods.Credits | TvShowMethods.ExternalIds);

			if (result == null)
			{
				throw new KeyNotFoundException($"TMDB ID {id} not found.");
			}

			var tvShow = mapper.Map<TVShow>(result);

			if (result.Images.Posters.Any())
			{
				tvShow.ImageUri = tmdbClient.GetImageUrl("original", result.Images.Posters.First().FilePath); // TODO: Move into AutoMapper ValueResolver
			}

			return tvShow;
		}

		public async Task<TVShow> GetAsync(string name)
		{
			var searchContainer = await tmdbClient.SearchTvShowAsync(name);

			if (!searchContainer.Results.Any())
			{
				throw new KeyNotFoundException($"No results for '{name}'");
			}

			return await GetAsync(searchContainer.Results.First().Id);
		}
	}
}