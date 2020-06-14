using Discord;
using Kinobot.Net.Models;
using System;
using System.Globalization;
using System.Linq;

namespace Kinobot.Net.Extensions
{
	public static class TVShowExtensions
	{
		private const int CastDisplayLimit = 6;
		private const int ProducerDisplayLimit = 3;
		private const string ZeroLengthSpace = "\u200b";

		public static Embed BuildDiscordEmbed(this TVShow tvShow)
		{
			if (tvShow == null)
			{
				throw new ArgumentNullException($"{nameof(tvShow)}");
			}

			var producers = tvShow.Crew.GetProducers().Take(CastDisplayLimit);

			var embedBuilder = new EmbedBuilder()
				.WithTitle(tvShow.Title)
				.WithDescription(tvShow.Description)
				.WithFooter(Properties.Resources.tmdbDisclaimer)
				.AddField("Air Date", tvShow.FirstAirDate.ToString("MMMM d, yyyy", CultureInfo.GetCultureInfo("en-US")))
				.AddField("Rating", tvShow.Rating, inline: true)
				.AddField("Seasons", tvShow.Seasons.Count(), inline: true)
				.AddField("Episodes", tvShow.Seasons.Sum(s => s.EpisodeCount), inline: true)
				.AddField("Genres", tvShow.Genres.Any() ? string.Join(", ", tvShow.Genres) : "N/A")
				.AddField("Producers", producers.Any() ? string.Join(", ", producers.Take(ProducerDisplayLimit).Select(credit => credit.Name)) : "N/A", inline: true);

			if (tvShow.Creators.Any())
			{
				embedBuilder.AddField("Created By", tvShow.Creators.Any() ? string.Join(", ", tvShow.Creators.Take(ProducerDisplayLimit).Select(credit => credit.Name)) : "N/A", inline: true);
			}

			if (tvShow.ImageUri != null)
			{
				embedBuilder.WithThumbnailUrl(tvShow.ImageUri.ToString());
			}

			if (tvShow.Cast.Any())
			{
				embedBuilder.AddField(ZeroLengthSpace, "Cast");
				foreach (var cast in tvShow.Cast.Take(CastDisplayLimit))
				{
					embedBuilder.AddField(cast.Role, cast.Name, inline: true);
				}
			}

			embedBuilder.AddField("Links", $"[IMDB]({tvShow.ImdbUri.ToString()}) [TMDB]({tvShow.TmdbUri.ToString()})");

			return embedBuilder.Build();
		}
	}
}