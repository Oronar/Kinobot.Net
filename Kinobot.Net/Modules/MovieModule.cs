using Discord.Commands;
using Kinobot.Net.Extensions;
using Kinobot.Net.Services.Contracts;
using System.Collections.Generic;
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
		[Summary("Search for a movie by title.")]
		public async Task SearchMovieAsync(params string[] query)
		{
			await ExecuteAsync(async () =>
			{
				try
				{
					var movie = await movieService.SearchAsync(string.Join(" ", query));
					await ReplyAsync(embed: movie.BuildDiscordEmbed());
				}
				catch (KeyNotFoundException e)
				{
					await loggingService.LogAsync(e.ToString());
					await ReplyAsync($"Move not found.");
				}
			});
		}
	}
}