using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class MovieProfileTests
	{
		private readonly IMapper mapper;

		public MovieProfileTests()
		{
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MovieProfile());
				cfg.AddProfile(new CreditProfile());
			});
			mapper = mapperConfig.CreateMapper();
		}

		#region TMDB Movie to Movie

		[Fact]
		public void CreateMap_Movie_Movie_TmdbIdIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Id, result.TmdbId);
		}

		[Fact]
		public void CreateMap_Movie_Movie_TitleIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Title, result.Title);
		}

		[Fact]
		public void CreateMap_Movie_Movie_DescriptionIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Overview, result.Description);
		}

		[Fact]
		public void CreateMap_Movie_Movie_ImdbUriMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal($"https://www.imdb.com/title/{source.ImdbId}", result.ImdbUri.ToString());
		}

		[Fact]
		public void CreateMap_Movie_Movie_TmdbUriMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal($"https://www.themoviedb.org/movie/{source.Id}", result.TmdbUri.ToString());
		}

		[Fact]
		public void CreateMap_Movie_Movie_ReleaseDateIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.ReleaseDate, result.ReleaseDate);
		}

		[Fact]
		public void CreateMap_Movie_Movie_GenresIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Genres.First().Name, result.Genres.First());
		}

		[Fact]
		public void CreateMap_Movie_Movie_RunTimeIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal((double)source.Runtime, result.RunTime.TotalMinutes);
		}

		[Fact]
		public void CreateMap_Movie_Movie_RatingIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.VoteAverage, result.Rating);
		}

		[Fact]
		public void CreateMap_Movie_Movie_BudgetIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Budget, result.Budget);
		}

		[Fact]
		public void CreateMap_Movie_Movie_RevenueIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Revenue, result.Revenue);
		}

		[Fact]
		public void CreateMap_Movie_Movie_CrewIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Credits.Crew.Count(), result.Crew.Count());
		}

		[Fact]
		public void CreateMap_Movie_Movie_FirstCrewIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Credits.Crew.First().Name, result.Crew.First().Name);
		}

		[Fact]
		public void CreateMap_Movie_Movie_FirstCastIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Credits.Cast.First().Name, result.Cast.First().Name);
		}

		[Fact]
		public void CreateMap_Movie_Movie_CastIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Credits.Cast.Count(), result.Cast.Count());
		}

		#endregion TMDB Movie to Movie

		#region TMDB SearchMovie to Movie

		[Fact]
		public void CreateMap_SearchMovie_Movie_TmdbDbIdIsMapped()
		{
			var source = BuildSourceSearchMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Id, result.TmdbId);
		}

		[Fact]
		public void CreateMap_SearchMovie_Movie_TitleIsMapped()
		{
			var source = BuildSourceSearchMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Title, result.Title);
		}

		[Fact]
		public void CreateMap_SearchMovie_Movie_DescriptionIsMapped()
		{
			var source = BuildSourceSearchMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Overview, result.Description);
		}

		[Fact]
		public void CreateMap_SearchMovie_Movie_RatingIsMapped()
		{
			var source = BuildSourceSearchMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.VoteAverage, result.Rating);
		}

		[Fact]
		public void CreateMap_SearchMovie_Movie_ReleaseDateIsMapped()
		{
			var source = BuildSourceSearchMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.ReleaseDate, result.ReleaseDate);
		}

		#endregion TMDB SearchMovie to Movie

		private TMDbLib.Objects.Movies.Movie BuildSourceMovie()
		{
			return new TMDbLib.Objects.Movies.Movie()
			{
				Id = 1,
				ImdbId = "tt0000000",
				Title = "title",
				Overview = "overview",
				Genres = new List<Genre>()
				{
					new Genre()
					{
						Name = "genre"
					}
				},
				Runtime = 1,
				VoteAverage = 5,
				ReleaseDate = DateTime.UtcNow,
				Credits = new TMDbLib.Objects.Movies.Credits()
				{
					Cast = new List<TMDbLib.Objects.Movies.Cast>
					{
						new TMDbLib.Objects.Movies.Cast(){
							Name = "name",
							Character = "character"
						}
					},
					Crew = new List<Crew>
					{
						new Crew()
						{
							Name = "name",
							Job = "job"
						}
					}
				}
			};
		}

		private SearchMovie BuildSourceSearchMovie()
		{
			return new SearchMovie()
			{
				Id = 1,
				Title = "Test Movie",
				Overview = "Test movie description.",
				VoteAverage = 5,
				ReleaseDate = DateTime.UtcNow
			};
		}
	}
}