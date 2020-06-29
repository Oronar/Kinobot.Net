using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kinobot.Net.Services
{
	public class TVService : ITVService
	{
		private readonly ITVRepository tvRepository;

		public TVService(ITVRepository tvRepository)
		{
			this.tvRepository = tvRepository;
		}

		public async Task<TVShow> GetAsync(int id)
		{
			return await tvRepository.GetAsync(id);
		}

		public async Task<TVShow> GetAsync(string title)
		{
			return await tvRepository.GetAsync(title);
		}

		public async Task<IEnumerable<TVShow>> SearchAsync(string title, int amount)
		{
			var pageCount = 1;
			var results = new List<TVShow>();
			do
			{
				var searchPage = await tvRepository.SearchAsync(title, pageCount);
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