using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class CreditProfileTests
	{
		[Fact]
		public void CreateMap_Cast_Credit_RoleIsMapped()
		{
			var cast = new Cast()
			{
				Character = "character"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(cast);

			Assert.Equal(cast.Character, result.Role);
		}

		[Fact]
		public void CreateMap_Crew_Credit_RoleIsMapped()
		{
			var crew = new Crew()
			{
				Job = "job"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(crew);

			Assert.Equal(crew.Job, result.Role);
		}
	}
}