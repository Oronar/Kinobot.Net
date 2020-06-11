using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Kinobot.Net.Tests.Services
{
	public class MovieServiceTests
	{
		[Fact]
		public void GetAsync_ReturnsMovie()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.GetAsync(It.IsAny<int>()))
				.ReturnsAsync(new Movie());
			var movieService = new MovieService(mockMovieRepository.Object);

			var result = movieService.GetAsync(1);

			Assert.NotNull(result);
		}

		[Fact]
		public void SearchAsynnc_ReturnsMovie()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), It.IsAny<int>()))
				.ReturnsAsync(new List<Movie>() { new Movie() });
			var movieService = new MovieService(mockMovieRepository.Object);

			var result = movieService.SearchAsync("title");

			Assert.NotNull(result);
		}
	}
}