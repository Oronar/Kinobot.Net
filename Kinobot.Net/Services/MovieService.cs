using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinobot.Net.Services
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository movieRepository;

		public MovieService(IMovieRepository movieRepository)
		{
			this.movieRepository = movieRepository;
		}

		public async Task<Movie> GetAsync(int id)
		{
			return await movieRepository.GetAsync(id);
		}

		public Task<IEnumerable<Movie>> SearchAsync(string query)
		{
			throw new NotImplementedException();
		}
	}
}