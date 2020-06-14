using Discord;
using Kinobot.Net.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinobot.Net.Extensions
{
	public static class TVShowExtensions
	{
		public static Embed BuildDiscordEmbed(this TVShow tvShow)
		{
			if (tvShow == null)
			{
				throw new ArgumentNullException($"{nameof(tvShow)}");
			}

			var builder = new EmbedBuilder()
				.WithTitle(tvShow.Title);

			return builder.Build();
		}
	}
}
