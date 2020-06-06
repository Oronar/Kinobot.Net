using Discord;
using Discord.Commands;
using Kinobot.Net.Services.Contracts;
using System;
using System.Collections.Generic;
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
				var embed = new EmbedBuilder()
					.WithTitle(movie.Title)
					.WithDescription(movie.Description)
					.WithFooter(Properties.Resources.tmdbDisclaimer)
					.WithImageUrl(movie.ImageUri.ToString())
					.AddField("Release Date", movie.ReleaseDate)
					.AddField("Genres", string.Join(", ", movie.Genres))
					.AddField("Runtime", movie.RunTime)
					.AddField("Rating", movie.Rating)
					.Build();

				await ReplyAsync(embed: embed);
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
					response.AppendLine($"{result.Title} ({result.Id})");
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