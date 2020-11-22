using AutoMapper;
using Kinobot.Net.Profiles;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class SearchPageProfileTests
	{
		[Fact]
		public void CreateMap_HasValidConfiguration()
		{
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new SearchPageProfile());
				cfg.AddProfile(new MovieProfile());
				cfg.AddProfile(new TVShowProfile());
				cfg.AddProfile(new CreditProfile());
				cfg.AddProfile(new SeasonSummaryProfile());
			});

			mapperConfig.AssertConfigurationIsValid();
		}
	}
}