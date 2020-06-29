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
				cfg.AddProfile(new TVShowProfile());
			});
			mapper = mapperConfig.CreateMapper();
		}

		#region SearchContainerMovie to SearchPageMovie

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_PageIsMapped()
		{
			var source = BuildSourceSearchContainerMovie();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.Page, result.Page);
		}

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_PageTotalIsMapped()
		{
			var source = BuildSourceSearchContainerMovie();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.TotalPages, result.PageTotal);
		}

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_ResultsIsMapped()
		{
			var source = BuildSourceSearchContainerMovie();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.Results.Count(), result.Results.Count());
		}

		[Fact]
		public void CreateMap_SearchContainerSearchMovie_SearchPageMovie_FirstResultIsMapped()
		{
			var source = BuildSourceSearchContainerMovie();

			var result = mapper.Map<SearchPage<Movie>>(source);

			Assert.Equal(source.Results.First().Title, result.Results.First().Title);
		}

		#endregion SearchContainerMovie to SearchPageMovie

		#region SearchContainerTv to SearchPageTVShow

		[Fact]
		public void CreateMap_SearchContainerSearchTv_SearchPageTVShow_PageIsMapped()
		{
			var source = BuildSourceSearchContainerTv();

			var result = mapper.Map<SearchPage<TVShow>>(source);

			Assert.Equal(source.Page, result.Page);
		}

		[Fact]
		public void CreateMap_SearchContainerSearchTv_SearchPageTVShow_PageTotalIsMapped()
		{
			var source = BuildSourceSearchContainerTv();

			var result = mapper.Map<SearchPage<TVShow>>(source);

			Assert.Equal(source.TotalPages, result.PageTotal);
		}

		[Fact]
		public void CreateMap_SearchContainerSearchTv_SearchPageTVShow_ResultsIsMapped()
		{
			var source = BuildSourceSearchContainerTv();

			var result = mapper.Map<SearchPage<TVShow>>(source);

			Assert.Equal(source.Results.Count(), result.Results.Count());
		}

		[Fact]
		public void CreateMap_SearchContainerSearchTv_SearchPageTVShow_FirstResultIsMapped()
		{
			var source = BuildSourceSearchContainerTv();

			var result = mapper.Map<SearchPage<TVShow>>(source);

			Assert.Equal(source.Results.First().Name, result.Results.First().Title);
		}

		#endregion SearchContainerTv to SearchPageTVShow

		private SearchContainer<SearchMovie> BuildSourceSearchContainerMovie()
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

		private SearchContainer<SearchTv> BuildSourceSearchContainerTv()
		{
			return new SearchContainer<SearchTv>()
			{
				Page = 1,
				TotalPages = 2,
				TotalResults = 5,
				Results = new List<SearchTv>()
				{
					new SearchTv()
					{
						Name = "Test Name"
					}
				}
			};
		}
	}
}