using Kinobot.Net.Models;
using System.Threading.Tasks;

namespace Kinobot.Net.Repositories.Contracts
{
	public interface ISearchRepository
	{
		Task<Media> MediaSearchAsync(string query);
	}
}