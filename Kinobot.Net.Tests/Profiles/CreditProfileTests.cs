using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using TMDbLib.Objects.General;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class CreditProfileTests
	{
		[Fact]
		public void CreateMap_Movies_Cast_Credit_RoleIsMapped()
		{
			var cast = new TMDbLib.Objects.Movies.Cast()
			{
				Character = "character"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(cast);

			Assert.Equal(cast.Character, result.Role);
		}

		[Fact]
		public void CreateMap_Movies_Cast_Credit_NameIsMapped()
		{
			var cast = new TMDbLib.Objects.Movies.Cast()
			{
				Name = "name"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(cast);

			Assert.Equal(cast.Name, result.Name);
		}

		[Fact]
		public void CreateMap_TvShows_Cast_Credit_RoleIsMapped()
		{
			var cast = new TMDbLib.Objects.TvShows.Cast()
			{
				Character = "character"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(cast);

			Assert.Equal(cast.Character, result.Role);
		}

		[Fact]
		public void CreateMap_TvShows_Cast_Credit_NameIsMapped()
		{
			var cast = new TMDbLib.Objects.TvShows.Cast()
			{
				Name = "name"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(cast);

			Assert.Equal(cast.Name, result.Name);
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

		[Fact]
		public void CreateMap_Crew_Credit_NameIsMapped()
		{
			var crew = new Crew()
			{
				Name = "name"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(crew);

			Assert.Equal(crew.Name, result.Name);
		}

		[Fact]
		public void CreateMap_CreatedBy_Credit_NameIsMapped()
		{
			var createdBy = new TMDbLib.Objects.TvShows.CreatedBy()
			{
				Name = "name"
			};
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CreditProfile()));
			var mapper = mapperConfig.CreateMapper();

			var result = mapper.Map<Credit>(createdBy);

			Assert.Equal(createdBy.Name, result.Name);
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