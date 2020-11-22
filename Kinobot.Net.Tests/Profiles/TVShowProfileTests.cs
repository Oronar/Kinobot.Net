using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class TVShowProfileTests
	{
		private readonly MapperConfiguration mapperConfiguration;
		private readonly IMapper mapper;

		public TVShowProfileTests()
		{
			mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new TVShowProfile());
				cfg.AddProfile(new CreditProfile());
				cfg.AddProfile(new SeasonSummaryProfile());
			});
			mapper = mapperConfiguration.CreateMapper();
		}

		[Fact]
		public void CreateMap_HasValidConfiguration()
		{
			mapperConfiguration.AssertConfigurationIsValid();
		}

		#region TMDB TvShow to TVShow

		[Fact]
		public void CreateMap_TvShow_TVShow_GenresIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Genres.Count(), result.Genres.Count());
		}

		#endregion TMDB TvShow to TVShow

		private TvShow BuildSourceTVShow()
		{
			return new TvShow()
			{
				Id = 1,
				Name = "Test Show",
				Overview = "Test show description.",
				ExternalIds = new ExternalIdsTvShow()
				{
					ImdbId = "tt0000000"
				},
				FirstAirDate = DateTime.UtcNow,
				VoteAverage = 5,
				Genres = new List<Genre>()
				{
					new Genre()
					{
						Id = 1,
						Name = "Adventure"
					},
					new Genre()
					{
						Id = 2,
						Name = "Action"
					},
				},
				CreatedBy = new List<CreatedBy>()
				{
					new CreatedBy()
					{
						Name = "John Doe"
					},
					new CreatedBy()
					{
						Name = "Jane Doe"
					}
				},
				Credits = new Credits()
				{
					Cast = new List<Cast>()
					{
						new Cast()
						{
							Name = "John Doe",
							Character = "Jack Doe"
						},
						new Cast()
						{
							Name = "Jane Doe",
							Character = "Sally Doe"
						}
					},
					Crew = new List<Crew>()
					{
						new Crew()
						{
							Name = "John Doe",
							Job = "Creator"
						},
						new Crew()
						{
							Name = "Jack Doe",
							Job = "Creator"
						}
					}
				},
				Seasons = new List<SearchTvSeason>()
				{
					new SearchTvSeason()
					{
						Id = 1,
						Name = "Season 1",
						AirDate = DateTime.UtcNow,
						EpisodeCount = 25,
						Overview = "Season overview.",
						SeasonNumber = 1
					},
					new SearchTvSeason()
					{
						Id = 1,
						Name = "Season 1",
						AirDate = DateTime.UtcNow,
						EpisodeCount = 25,
						Overview = "Season overview.",
						SeasonNumber = 2
					}
				}
			};
		}
	}
}