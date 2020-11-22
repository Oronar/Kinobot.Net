using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class CreditProfileTests
	{
		[Fact]
		public void CreateMap_HasValidConfiguration()
		{
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			mapperConfig.AssertConfigurationIsValid();
		}

		[Fact]
		public void CreateMap_CreatedBy_Credit_RoleIsMapped()
		{
			var createdBy = new TMDbLib.Objects.TvShows.CreatedBy()
			{
				Name = "name"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(createdBy);

			Assert.Equal("Creator", result.Role);
		}
	}
}