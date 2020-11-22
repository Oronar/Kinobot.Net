using Discord.Commands;
using Kinobot.Net.Extensions;
using Kinobot.Net.Models;
using Kinobot.Net.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinobot.Net.Modules
{

	public class MediaModule : BaseModule
	{
		private readonly ISearchService searchService;

		public MediaModule(ILoggingService loggingService, ISearchService searchService) : base(loggingService)
		{
			this.searchService = searchService;
		}

		[Command("search")]
		[Summary("Search all media types")]
		public async Task GetMovieAsync(string query)
		{
			await ExecuteAsync(async () =>
			{
				try
				{
					var media = await searchService.MediaSearchAsync(query);
					var embed = media.MediaType == MediaType.Movie ? ((Movie)media).BuildDiscordEmbed() : ((TVShow)media).BuildDiscordEmbed();
					await ReplyAsync(embed: embed);
				}
				catch (KeyNotFoundException e)
				{
					await loggingService.LogAsync(e.ToString());
					await ReplyAsync($"Media not found: {e.Message}");
				}
			});
		}
	}
}