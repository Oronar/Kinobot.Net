using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using Xunit;

namespace Kinobot.Net.Tests.Profiles
{
	public class MovieProfileTests
	{
		private readonly MapperConfiguration mapperConfiguration;
		private readonly IMapper mapper;

		public MovieProfileTests()
		{
			mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MovieProfile());
				cfg.AddProfile(new CreditProfile());
			});
			mapper = mapperConfiguration.CreateMapper();
		}

		[Fact]
		public void CreateMap_HasValidConfiguration()
		{
			mapperConfiguration.AssertConfigurationIsValid();
		}

		#region TMDB Movie to Movie

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

		#endregion TMDB Movie to Movie

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
	}
}