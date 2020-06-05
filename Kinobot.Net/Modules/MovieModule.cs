using Discord;
using Discord.Commands;
using Kinobot.Net.Modules.Contracts;
using Kinobot.Net.Services.Contracts;
using System;
using System.Linq;
using System.Text;
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
				var embed = new EmbedBuilder()
					.WithTitle(movie.Title)
					.WithDescription(movie.Description)
					.WithFooter("Data via https://www.themoviedb.org")
					.WithImageUrl(movie.ImageUrl)
					.AddField("Release Date", movie.ReleaseDate)
					.AddField("Genres", string.Join(", ", movie.Genres))
					.Build();

				await ReplyAsync(embed: embed);
			}
			catch (Exception e)
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
			catch (Exception e)
			{
				await loggingService.LogAsync(e.ToString());
				await ReplyAsync($"Move not found: {e.Message}");
			}
		}
	}
}