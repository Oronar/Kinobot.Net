using Discord.Commands;
using Kinobot.Net.Extensions;
using Kinobot.Net.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	[Group("movie")]
	public class MovieModule : BaseModule
	{
		private readonly IMovieService movieService;

		public MovieModule(ILoggingService loggingService, IMovieService movieService) : base(loggingService)
		{
			this.movieService = movieService;
		}

		[Command("tmdb")]
		[Summary("Retrieve movie by TMDB ID")]
		public async Task GetMovieAsync(int id)
		{
			await ExecuteAsync(async () =>
			{
				try
				{
					var movie = await movieService.GetAsync(id);
					await ReplyAsync(embed: movie.BuildDiscordEmbed());
				}
				catch (KeyNotFoundException e)
				{
					await loggingService.LogAsync(e.ToString());
					await ReplyAsync($"Movie not found: {e.Message}");
				}
			});
		}

		[Command]
		[Summary("Retrieve a movie by title.")]
		public async Task GetMovieAsync(params string[] title)
		{
			await ExecuteAsync(async () =>
			{
				try
				{
					var movie = await movieService.GetAsync(string.Join(" ", title));
					await ReplyAsync(embed: movie.BuildDiscordEmbed());
				}
				catch (KeyNotFoundException e)
				{
					await loggingService.LogAsync(e.ToString());
					await ReplyAsync($"Movie not found.");
				}
			});
		}

		[Command("list")]
		[Priority(1)]
		[Summary("Lists movies by title.")]
		public async Task ListMoviesAsync(params string[] title)
		{
			await ExecuteAsync(async () =>
			{
				var movies = await movieService.SearchAsync(string.Join(" ", title), 10);
				await DotLeaderReplyAsync(movies.ToDictionary(m => $"{m.Title} ({m.ReleaseDate.Year})", m => $"(TMDB ID: {m.TmdbId})"));
			});
		}
	}
}