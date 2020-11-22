using Kinobot.Net.Models;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services;
using Moq;
using Xunit;

namespace Kinobot.Net.Tests.Services
{
	public class SearchServiceTests
	{
		[Fact]
		public void GetAsync_MediaSearchAsync_ReturnsMedia()
		{
			var mockSearchRepository = new Mock<ISearchRepository>();
			mockSearchRepository.Setup(m => m.MediaSearchAsync(It.IsAny<string>()))
				.ReturnsAsync(new Movie())
				.Verifiable();
			var movieService = new SearchService(mockSearchRepository.Object);

			var result = movieService.MediaSearchAsync("query");

			Assert.NotNull(result);

			mockSearchRepository.Verify();
		}
	}
}