using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using System;
using TMDbLib.Objects.Search;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class SeasonSummaryProfileTests
	{
		private readonly IMapper mapper;

		public SeasonSummaryProfileTests()
		{
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new SeasonSummaryProfile());
			});
			mapper = mapperConfig.CreateMapper();
		}

		[Fact]
		public void CreateMap_SearchTvSeason_SeasonSummary_TmdbIdIsMapped()
		{
			var source = BuildSourceSearchTvSeason();

			var result = mapper.Map<SeasonSummary>(source);

			Assert.Equal(source.Id, result.TmdbId);
		}

		[Fact]
		public void CreateMap_SearchTvSeason_SeasonSummary_SeasonNumberIsMapped()
		{
			var source = BuildSourceSearchTvSeason();

			var result = mapper.Map<SeasonSummary>(source);

			Assert.Equal(source.SeasonNumber, result.SeasonNumber);
		}

		[Fact]
		public void CreateMap_SearchTvSeason_SeasonSummary_NameIsMapped()
		{
			var source = BuildSourceSearchTvSeason();

			var result = mapper.Map<SeasonSummary>(source);

			Assert.Equal(source.Name, result.Name);
		}

		[Fact]
		public void CreateMap_SearchTvSeason_SeasonSummary_DescriptionIsMapped()
		{
			var source = BuildSourceSearchTvSeason();

			var result = mapper.Map<SeasonSummary>(source);

			Assert.Equal(source.Overview, result.Description);
		}

		[Fact]
		public void CreateMap_SearchTvSeason_SeasonSummary_EpisodeCountIsMapped()
		{
			var source = BuildSourceSearchTvSeason();

			var result = mapper.Map<SeasonSummary>(source);

			Assert.Equal(source.EpisodeCount, result.EpisodeCount);
		}

		[Fact]
		public void CreateMap_SearchTvSeason_SeasonSummary_AirDateIsMapped()
		{
			var source = BuildSourceSearchTvSeason();

			var result = mapper.Map<SeasonSummary>(source);

			Assert.Equal(source.AirDate, result.AirDate);
		}

		private SearchTvSeason BuildSourceSearchTvSeason()
		{
			return new SearchTvSeason()
			{
				Id = 1,
				SeasonNumber = 1,
				EpisodeCount = 25,
				Name = "Season 1",
				AirDate = DateTime.UtcNow,
				Overview = "Season 1 overview."
			};
		}
	}
}