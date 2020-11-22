using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using MediaType = TMDbLib.Objects.General.MediaType;

namespace Kinobot.Net.Repositories
{
	public class TMDbSearchRepository : ISearchRepository
	{
		private readonly TMDbClient tmdbClient;
		private readonly IMovieRepository movieRepository;
		private readonly ITVRepository tvRepository;

		public TMDbSearchRepository(TMDbClient tmdbClient, IMovieRepository movieRepository, ITVRepository tvRepository)
		{
			this.tmdbClient = tmdbClient ?? throw new ArgumentNullException($"{nameof(tmdbClient)}");
			this.tmdbClient.GetConfigAsync();

			this.movieRepository = movieRepository ?? throw new ArgumentNullException($"{nameof(movieRepository)}");
			this.tvRepository = tvRepository ?? throw new ArgumentNullException($"{nameof(tvRepository)}");
		}

		public async Task<Media> MediaSearchAsync(string query)
		{
			var searchContainer = await tmdbClient.SearchMultiAsync(query);

			if (!searchContainer.Results.Any())
			{
				throw new KeyNotFoundException($"No results for '{query}'");
			}

			var result = searchContainer.Results.First();

			switch (searchContainer.Results.First().MediaType)
			{
				case (MediaType.Movie):
					return await movieRepository.GetAsync(result.Id);

				case (MediaType.Tv):
					return await tvRepository.GetAsync(result.Id);

				default:
					throw new KeyNotFoundException($"Unsupported media type ({result.MediaType}) for '{query}'");
			}
		}
	}
}