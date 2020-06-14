using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services.Contracts;
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
	}
}