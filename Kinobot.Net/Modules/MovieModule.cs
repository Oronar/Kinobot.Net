using Discord;
using Discord.Commands;
using Kinobot.Net.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	public class MovieModule : ModuleBase<SocketCommandContext>
	{
		private readonly ILoggingService loggingService;
		private readonly IMovieService movieService;

		public MovieModule(ILoggingService loggingService, IMovieService movieService)
		{
			this.loggingService = loggingService;
			this.movieService = movieService;
		}

		[Command("movie")]
		[Summary("Retrieve movie by TMDB ID")]
		public async Task GetMovieAsync(int id)
		{
			try
			{
				var movie = await movieService.GetAsync(id);
				var embedBuilder = new EmbedBuilder()
					.WithTitle(movie.Title)
					.WithDescription(movie.Description)
					.WithFooter(Properties.Resources.tmdbDisclaimer)
					.WithThumbnailUrl(movie.ImageUri.ToString())
					.AddField("Release Date", movie.ReleaseDate.ToString("MMMM d, yyyy", CultureInfo.GetCultureInfo("en-US")), inline: true)
					.AddField("Runtime", $"{movie.RunTime:%h}h {movie.RunTime:mm}m", inline: true)
					.AddField("Rating", movie.Rating, inline: true)
					.AddField("Genres", string.Join(", ", movie.Genres))
					.AddField("Budget", movie.Budget.ToString("C0", CultureInfo.GetCultureInfo("en-US")), inline: true)
					.AddField("Revenue", movie.Revenue.ToString("C0", CultureInfo.GetCultureInfo("en-US")), inline: true)
					.AddField("\u200b", "Crew")
					.AddField("Director", string.Join(", ", movie.Directors.Take(3)), inline: true)
					.AddField("Screenplay", string.Join(", ", movie.ScreenplayWriters.Take(3)), inline: true)
					.AddField("\u200b", "Cast");

				foreach (var cast in movie.Cast.Take(3))
				{
					embedBuilder.AddField(cast.Role, cast.Name, inline: true);
				}

				embedBuilder.AddField("Links", $"[IMDB]({movie.ImdbUri.ToString()}) [TMDB]({movie.TmdbUri.ToString()})");

				await ReplyAsync(embed: embedBuilder.Build());
			}
			catch (KeyNotFoundException e)
			{
				await loggingService.LogAsync(e.ToString());
				await ReplyAsync($"Movie not found: {e.Message}");
			}
		}

		[Command("movie")]
		[Summary("Search movie by title.")]
		public async Task SearchMovieAsync(params string[] query)
		{
			try
			{
				var results = await movieService.SearchAsync(string.Join(" ", query));

				var response = new StringBuilder($"{results.Count()} results:{Environment.NewLine}");
				foreach (var result in results)
				{
					response.AppendLine($"{result.Title} ({result.TmdbId})");
				}

				await ReplyAsync(response.ToString());
			}
			catch (KeyNotFoundException e)
			{
				await loggingService.LogAsync(e.ToString());
				await ReplyAsync($"Move not found: {e.Message}");
			}
		}
	}
}