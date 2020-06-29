using Discord.Commands;
using Kinobot.Net.Extensions;
using Kinobot.Net.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{
	[Group("tv")]
	public class TVModule : BaseModule
	{
		private readonly ITVService tvService;

		public TVModule(ILoggingService loggingService, ITVService tvService) : base(loggingService)
		{
			this.tvService = tvService;
		}

		[Command("tmdb")]
		[Summary("Retrieve TV show by TMDB ID")]
		public async Task GetTVShowAsync(int id)
		{
			await ExecuteAsync(async () =>
			{
				try
				{
					var tvShow = await tvService.GetAsync(id);
					await ReplyAsync(embed: tvShow.BuildDiscordEmbed());
				}
				catch (KeyNotFoundException e)
				{
					await loggingService.LogAsync(e.ToString());
					await ReplyAsync($"TV show not found: {e.Message}");
				}
			});
		}

		[Command]
		[Summary("Retrieve a TV show by title.")]
		public async Task GetTVShowAsync(params string[] title)
		{
			await ExecuteAsync(async () =>
			{
				try
				{
					var tvShow = await tvService.GetAsync(string.Join(" ", title));
					await ReplyAsync(embed: tvShow.BuildDiscordEmbed());
				}
				catch (KeyNotFoundException e)
				{
					await loggingService.LogAsync(e.ToString());
					await ReplyAsync($"TV show not found.");
				}
			});
		}

		[Command("list")]
		[Priority(1)]
		[Summary("Lists TV shows by title.")]
		public async Task ListMoviesAsync(params string[] title)
		{
			await ExecuteAsync(async () =>
			{
				var shows = await tvService.SearchAsync(string.Join(" ", title), 10);
				await DotLeaderReplyAsync(shows.ToDictionary(s => s.Title, s => $"({s.FirstAirDate.Year})(TMDB ID: {s.TmdbId})"));
			});
		}
	}
}