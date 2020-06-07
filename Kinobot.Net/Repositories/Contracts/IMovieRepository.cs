using Kinobot.Net.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinobot.Net.Repositories.Contracts
{
	public interface IMovieRepository
	{
		Task<Movie> GetAsync(int id);

		Task<Movie> GetAsync(string name);

		Task<IEnumerable<Movie>> SearchAsync(string query, int page = 0);

		Uri GetImageUrl(string filePath, string size = "original");
	}
}