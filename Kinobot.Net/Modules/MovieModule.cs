using Discord.Commands;
using Kinobot.Net.Extensions;
using Kinobot.Net.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	[Group("movie")]
	public class MovieModule : ModuleBase<SocketCommandContext>
	{
		private readonly ILoggingService loggingService;
		private readonly IMovieService movieService;

		public MovieModule(ILoggingService loggingService, IMovieService movieService)
		{
			this.loggingService = loggingService;
			this.movieService = movieService;
		}

		[Command("tmdb")]
		[Summary("Retrieve movie by TMDB ID")]
		public async Task GetMovieAsync(int id)
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
		}

		[Command]
		[Summary("Search movie by title.")]
		public async Task SearchMovieAsync(params string[] query)
		{
			try
			{
				var movie = await movieService.SearchAsync(string.Join(" ", query));

				await ReplyAsync(embed: movie.BuildDiscordEmbed());
			}
			catch (KeyNotFoundException e)
			{
				await loggingService.LogAsync(e.ToString());
				await ReplyAsync($"Move not found: {e.Message}");
			}
		}
	}
}