using Kinobot.Net.Models;
using System.Threading.Tasks;

namespace Kinobot.Net.Services.Contracts
{
	public interface ISearchService
	{
		Task<Media> MediaSearchAsync(string query);
	}
}