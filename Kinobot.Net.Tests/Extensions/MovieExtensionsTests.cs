using Kinobot.Net.Extensions;
using Kinobot.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Kinobot.Net.Tests.Extensions
{
	public class MovieExtensionsTests
	{
		[Fact]
		public void BuildDiscordEmbed_WithValidMovie_ReturnsEmbed()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.NotNull(embed);
		}

		[Fact]
		public void BuildDiscordEmbed_WithNullMovie_ThrowsException()
		{
			Movie movie = null;

			var exception = Assert.Throws<ArgumentNullException>(() => movie.BuildDiscordEmbed());

			Assert.Equal("Value cannot be null. (Parameter 'movie')", exception.Message);
		}

		[Fact]
		public void BuildDiscordEmbed_WithTitle_TitleIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Title, embed.Title);
		}

		[Fact]
		public void BuildDiscordEmbed_WithDescription_DescritpionIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Description, embed.Description);
		}

		[Fact]
		public void BuildDiscordEmbed_WithTMDBDisclaimer_FooterIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal("Data via https://www.themoviedb.org", embed.Footer.ToString());
		}

		[Fact]
		public void BuildDiscordEmbed_WithReleaseDate_ReleaseDateIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.ReleaseDate.ToString("MMMM d, yyyy"), embed.Fields.First(f => f.Name.Equals("Release Date")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithRuntime_RuntimeIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal($"{movie.RunTime:%h}h {movie.RunTime:mm}m", embed.Fields.First(f => f.Name.Equals("Runtime")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithRating_RatingIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Rating.ToString(), embed.Fields.First(f => f.Name.Equals("Rating")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithBudget_BudgetIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Budget.ToString("C0"), embed.Fields.First(f => f.Name.Equals("Budget")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithRevenue_RevenueIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Revenue.ToString("C0"), embed.Fields.First(f => f.Name.Equals("Revenue")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithGenres_GenresIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(string.Join(", ", movie.Genres), embed.Fields.First(f => f.Name.Equals("Genres")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithNoGenres_GenresIsNotApplicable()
		{
			var movie = BuildMovie();
			movie.Genres = Enumerable.Empty<string>();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal("N/A", embed.Fields.First(f => f.Name.Equals("Genres")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithDirector_DirectorIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Crew.First(c => c.Role.Equals("Director")).Name, embed.Fields.First(f => f.Name.Equals("Director")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithMultipleDirector_DirectorIsSet()
		{
			var movie = BuildMovie();
			movie.Crew = new List<Credit>()
			{
				new Credit()
				{
					Name = "John Doe",
					Role = "Director"
				},
				new Credit()
				{
					Name = "Jane Doe",
					Role = "Screenplay"
				},
				new Credit()
				{
					Name = "Philip Doe",
					Role = "Director"
				}
			};

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(string.Join(", ", movie.Crew.Where(c => c.Role.Equals("Director")).Select(c => c.Name)), embed.Fields.First(f => f.Name.Equals("Director")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithNoDirector_DirectorIsNotApplicable()
		{
			var movie = BuildMovie();
			movie.Crew = Enumerable.Empty<Credit>();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal("N/A", embed.Fields.First(f => f.Name.Equals("Director")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithScreenplay_ScreenplayIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Crew.First(c => c.Role.Equals("Screenplay")).Name, embed.Fields.First(f => f.Name.Equals("Screenplay")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithMultipleScreenplay_ScreenplayIsSet()
		{
			var movie = BuildMovie();
			movie.Crew = new List<Credit>()
			{
				new Credit()
				{
					Name = "John Doe",
					Role = "Director"
				},
				new Credit()
				{
					Name = "Jane Doe",
					Role = "Screenplay"
				},
				new Credit()
				{
					Name = "Philip Doe",
					Role = "Screenplay"
				}
			};

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(string.Join(", ", movie.Crew.Where(c => c.Role.Equals("Screenplay")).Select(c => c.Name)), embed.Fields.First(f => f.Name.Equals("Screenplay")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithNoScreenplay_ScreenplayIsNotApplicable()
		{
			var movie = BuildMovie();
			movie.Crew = Enumerable.Empty<Credit>();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal("N/A", embed.Fields.First(f => f.Name.Equals("Screenplay")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithImageUri_ThumbnailIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.ImageUri.ToString(), embed.Thumbnail.ToString());
		}

		[Fact]
		public void BuildDiscordEmbed_WithNullImageUri_ThumbnailIsNotSet()
		{
			var movie = BuildMovie();
			movie.ImageUri = null;

			var embed = movie.BuildDiscordEmbed();

			Assert.False(embed.Thumbnail.HasValue);
		}

		[Fact]
		public void BuildDiscordEmbed_WithCast_CharacterIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal(movie.Cast.First().Name, embed.Fields.First(f => f.Name.Equals(movie.Cast.First().Role)).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithCast_CastIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal("Cast", embed.Fields.First(f => f.Name.Equals("\u200b")).Value);
		}

		[Fact]
		public void BuildDiscordEmbed_WithNoCast_CastIsNotSet()
		{
			var movie = BuildMovie();
			movie.Cast = Enumerable.Empty<Credit>();

			var embed = movie.BuildDiscordEmbed();

			Assert.False(embed.Fields.Any(f => f.Name.Equals("\u200b")));
		}

		[Fact]
		public void BuildDiscordEmbed_WithLinks_LinksIsSet()
		{
			var movie = BuildMovie();

			var embed = movie.BuildDiscordEmbed();

			Assert.Equal("[IMDB](https://www.imdb.com/title/tt0000000) [TMDB](https://www.themoviedb.org/movie/1)", embed.Fields.First(f => f.Name.Equals("Links")).Value);
		}

		private Movie BuildMovie()
		{
			return new Movie()
			{
				TmdbId = 1,
				ImdbId = "tt0000000",
				Title = "Test Movie",
				Description = "An exciting test movie.",
				ImageUri = new Uri("http://example.com/image.jpg"),
				ReleaseDate = DateTime.UtcNow,
				Genres = new List<string>()
				{
					"Action", "Adventure"
				},
				RunTime = TimeSpan.FromMinutes(90),
				Rating = 5,
				Budget = 1000000,
				Revenue = 2000000,
				Crew = new List<Credit>()
				{
					new Credit()
					{
						Name = "John Doe",
						Role = "Director"
					},
					new Credit()
					{
						Name = "Jane Doe",
						Role = "Screenplay"
					}
				},
				Cast = new List<Credit>()
				{
					new Credit()
					{
						Name = "Jake Doe",
						Role = "Amazing Jake"
					}
				}
			};
		}
	}
}