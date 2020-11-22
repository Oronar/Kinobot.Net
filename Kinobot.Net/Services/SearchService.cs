using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Kinobot.Net.Services
{
	public class SearchService : ISearchService
	{
		private readonly ISearchRepository searchRepository;

		public SearchService(ISearchRepository searchRepository)
		{
			this.searchRepository = searchRepository ?? throw new ArgumentNullException($"{nameof(searchRepository)}"); ;
		}

		public async Task<Media> MediaSearchAsync(string query)
		{
			return await searchRepository.MediaSearchAsync(query);
		}
	}
}