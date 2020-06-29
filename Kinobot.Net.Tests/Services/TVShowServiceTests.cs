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
	public class TVShowServiceTests
	{
		[Fact]
		public void GetAsync_WithId_ReturnsTVShow()
		{
			var mockTvRepository = new Mock<ITVRepository>();
			mockTvRepository.Setup(m => m.GetAsync(It.IsAny<int>()))
				.ReturnsAsync(new TVShow());
			var TVService = new TVService(mockTvRepository.Object);

			var result = TVService.GetAsync(1);

			Assert.NotNull(result);
		}

		[Fact]
		public void GetAsync_WithTitle_ReturnsTVShow()
		{
			var mockTvRepository = new Mock<ITVRepository>();
			mockTvRepository.Setup(m => m.GetAsync(It.IsAny<string>()))
				.ReturnsAsync(new TVShow());
			var TVService = new TVService(mockTvRepository.Object);

			var result = TVService.GetAsync("title");

			Assert.NotNull(result);
		}

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsLessThanTotal_ReturnsSearchPage()
		{
			var mockTVRepository = new Mock<ITVRepository>();
			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow() }
				});

			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow() }
				});

			var TVService = new TVService(mockTVRepository.Object);

			var results = await TVService.SearchAsync("title", 1);

			Assert.Single(results);
		}

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsEqualToTotal_ReturnsSearchPage()
		{
			var mockTVRepository = new Mock<ITVRepository>();
			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow() }
				});

			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow() }
				});

			var TVService = new TVService(mockTVRepository.Object);

			var results = await TVService.SearchAsync("title", 2);

			Assert.Equal(2, results.Count());
		}

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsGreaterThanTotal_ReturnsSearchPage()
		{
			var mockTVRepository = new Mock<ITVRepository>();
			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow() }
				});

			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow() }
				});

			var TVService = new TVService(mockTVRepository.Object);

			var results = await TVService.SearchAsync("title", 5);

			Assert.Equal(2, results.Count());
		}

		[Fact]
		public async Task SearchAsync_WhenRequestAmountIsLessThanPage_ReturnsSearchPage()
		{
			var mockTVRepository = new Mock<ITVRepository>();
			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 1))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 1,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow(), new TVShow() }
				});

			mockTVRepository.Setup(m => m.SearchAsync(It.IsAny<string>(), 2))
				.ReturnsAsync(new SearchPage<TVShow>()
				{
					Page = 2,
					PageTotal = 2,
					Results = new List<TVShow>() { new TVShow(), new TVShow() }
				});

			var TVService = new TVService(mockTVRepository.Object);

			var results = await TVService.SearchAsync("title", 1);

			Assert.Single(results);
		}
	}
}