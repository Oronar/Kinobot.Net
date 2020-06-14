using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services;
using Moq;
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
			var movieService = new TVService(mockTvRepository.Object);

			var result = movieService.GetAsync(1);

			Assert.NotNull(result);
		}

		[Fact]
		public void GetAsync_WithTitle_ReturnsTVShow()
		{
			var mockTvRepository = new Mock<ITVRepository>();
			mockTvRepository.Setup(m => m.GetAsync(It.IsAny<string>()))
				.ReturnsAsync(new TVShow());
			var movieService = new TVService(mockTvRepository.Object);

			var result = movieService.GetAsync("title");

			Assert.NotNull(result);
		}
	}
}