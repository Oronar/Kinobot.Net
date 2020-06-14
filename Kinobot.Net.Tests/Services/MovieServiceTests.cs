using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services;
using Moq;
using Xunit;

namespace Kinobot.Net.Tests.Services
{
	public class MovieServiceTests
	{
		[Fact]
		public void GetAsync_WithId_ReturnsMovie()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.GetAsync(It.IsAny<int>()))
				.ReturnsAsync(new Movie());
			var movieService = new MovieService(mockMovieRepository.Object);

			var result = movieService.GetAsync(1);

			Assert.NotNull(result);
		}

		[Fact]
		public void GetAsync_WithTitle_ReturnsMovie()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.GetAsync(It.IsAny<string>()))
				.ReturnsAsync(new Movie());
			var movieService = new MovieService(mockMovieRepository.Object);

			var result = movieService.GetAsync("title");

			Assert.NotNull(result);
		}
	}
}