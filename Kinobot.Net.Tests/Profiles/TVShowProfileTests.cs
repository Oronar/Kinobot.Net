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
		private readonly IMapper mapper;

		public TVShowProfileTests()
		{
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new TVShowProfile());
				cfg.AddProfile(new CreditProfile());
				cfg.AddProfile(new SeasonSummaryProfile());
			});
			mapper = mapperConfig.CreateMapper();
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_TmdbIdIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Id, result.TmdbId);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_TitleIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Name, result.Title);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_DescriptionIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Overview, result.Description);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_ImdbIdIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.ExternalIds.ImdbId, result.ImdbId);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_FirstAirDateIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.FirstAirDate, result.FirstAirDate);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_GenresIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Genres.Count(), result.Genres.Count());
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_FirstGenreIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Genres.First().Name, result.Genres.First());
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_CreatorsIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.CreatedBy.Count(), result.Creators.Count());
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_FirstCreatorIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.CreatedBy.First().Name, result.Creators.First().Name);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_CrewIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Credits.Crew.Count(), result.Crew.Count());
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_FirstCrewIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Credits.Crew.First().Name, result.Crew.First().Name);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_CastIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Credits.Cast.Count(), result.Cast.Count());
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_FirstCastIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Credits.Cast.First().Name, result.Cast.First().Name);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_SeasonsIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Seasons.Count(), result.Seasons.Count());
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_FirstSeasonIsMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.Seasons.First().SeasonNumber, result.Seasons.First().SeasonNumber);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_RatingMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal(source.VoteAverage, result.Rating);
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_ImdbUriMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal($"https://www.imdb.com/title/{source.ExternalIds.ImdbId}", result.ImdbUri.ToString());
		}

		[Fact]
		public void CreateMap_TvShow_TVShow_TmdbUriMapped()
		{
			var source = BuildSourceTVShow();

			var result = mapper.Map<TVShow>(source);

			Assert.Equal($"https://www.themoviedb.org/tv/{source.Id}", result.TmdbUri.ToString());
		}

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