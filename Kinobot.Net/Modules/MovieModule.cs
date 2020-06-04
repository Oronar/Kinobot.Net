using Discord.Commands;
using Kinobot.Net.Modules.Contracts;
using Kinobot.Net.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	public class MovieModule : ModuleBase<SocketCommandContext>, IMovieModule
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

				await ReplyAsync($"Movie Found: {movie.Title}");
			}
			catch (Exception e)
			{
				await loggingService.LogStringAsync(e.ToString());
				await ReplyAsync($"Movie not found: {e.Message}");
			}
		}

		public Task SearchMovieAsync(string query)
		{
			throw new NotImplementedException();
		}
	}
}