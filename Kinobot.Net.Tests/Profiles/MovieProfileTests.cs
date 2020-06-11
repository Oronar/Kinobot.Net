using AutoMapper;
using Kinobot.Net.Models;
using Kinobot.Net.Profiles;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
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

		[Fact]
		public void CreateMap_Movie_Movie_TmdbIdIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Id, result.TmdbId);
		}

		[Fact]
		public void CreateMap_Movie_Movie_DescriptionIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Overview, result.Description);
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
		public void CreateMap_Movie_Movie_CrewIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Credits.Crew.First().Name, result.Crew.First().Name);
		}

		[Fact]
		public void CreateMap_Movie_Movie_CastIsMapped()
		{
			var source = BuildSourceMovie();

			var result = mapper.Map<Movie>(source);

			Assert.Equal(source.Credits.Cast.First().Name, result.Cast.First().Name);
		}

		private TMDbLib.Objects.Movies.Movie BuildSourceMovie()
		{
			return new TMDbLib.Objects.Movies.Movie()
			{
				Id = 1,
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