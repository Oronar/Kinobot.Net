using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsLessThanTotal_ReturnsSearchPage()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie() }
				});

			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie() }
				});

			var movieService = new MovieService(mockMovieRepository.Object);

			var results = await movieService.SearchAsync("title", 1);

			Assert.Single(results);
		}

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsEqualToTotal_ReturnsSearchPage()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie() }
				});

			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie() }
				});

			var movieService = new MovieService(mockMovieRepository.Object);

			var results = await movieService.SearchAsync("title", 2);

			Assert.Equal(2, results.Count());
		}

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsGreaterThanTotal_ReturnsSearchPage()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie() }
				});

			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie() }
				});

			var movieService = new MovieService(mockMovieRepository.Object);

			var results = await movieService.SearchAsync("title", 5);

			Assert.Equal(2, results.Count());
		}

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsLessThanPage_ReturnsSearchPage()
		{
			var mockMovieRepository = new Mock<IMovieRepository>();
			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie(), new Movie() }
				});

			mockMovieRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<Movie>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<Movie>() { new Movie(), new Movie() }
				});

			var movieService = new MovieService(mockMovieRepository.Object);

			var results = await movieService.SearchAsync("title", 1);

			Assert.Single(results);
		}
	}
}