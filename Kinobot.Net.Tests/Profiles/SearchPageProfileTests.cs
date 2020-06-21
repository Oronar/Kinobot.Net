using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class SearchPageProfileTests
	{
		private readonly IMapper mapper;

		public SearchPageProfileTests()
		{
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new SearchPageProfile());
				cfg.AddProfile(new MovieProfile());
			});
			mapper = mapperConfig.CreateMapper();
		}

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_PageIsMapped()
		{
			var source = BuildSourceSearchContainer();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.Page, result.Page);
		}

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_PageTotalIsMapped()
		{
			var source = BuildSourceSearchContainer();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.TotalPages, result.PageTotal);
		}

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_ResultsIsMapped()
		{
			var source = BuildSourceSearchContainer();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.Results.Count(), result.Results.Count());
		}

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_FirstResultIsMapped()
		{
			var source = BuildSourceSearchContainer();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.Results.First().Title, result.Results.First().Title);
		}

		private SearchContainer<SearchMovie> BuildSourceSearchContainer()
		{
			return new SearchContainer<SearchMovie>()
			{
				Page = 1,
				TotalPages = 2,
				TotalResults = 5,
				Results = new List<SearchMovie>()
				{
					new SearchMovie()
					{
						Title = "Test Title"
					}
				}
			};
		}
	}
}