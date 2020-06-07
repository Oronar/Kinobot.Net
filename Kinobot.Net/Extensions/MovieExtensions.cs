using Discord;
using Kinobot.Net.Models;
using System;
using System.Globalization;
using System.Linq;

namespace Kinobot.Net.Extensions
{
	public static class MovieExtensions
	{
		private const int DisplayLimit = 3;
		private const string ZeroLengthSpace = "\u200b";

		public static Embed BuildDiscordEmbed(this Movie movie)
		{
			if (movie == null)
			{
				throw new ArgumentNullException($"{nameof(movie)} cannot be null.");
			}

			var embedBuilder = new EmbedBuilder()
				.WithTitle(movie.Title)
				.WithDescription(movie.Description)
				.WithFooter(Properties.Resources.tmdbDisclaimer)
				.WithThumbnailUrl(movie.ImageUri.ToString())
				.AddField("Release Date", movie.ReleaseDate.ToString("MMMM d, yyyy", CultureInfo.GetCultureInfo("en-US")), inline: true)
				.AddField("Runtime", $"{movie.RunTime:%h}h {movie.RunTime:mm}m", inline: true)
				.AddField("Rating", movie.Rating, inline: true)
				.AddField("Budget", movie.Budget.ToString("C0", CultureInfo.GetCultureInfo("en-US")), inline: true)
				.AddField("Revenue", movie.Revenue.ToString("C0", CultureInfo.GetCultureInfo("en-US")), inline: true)
				.AddField("Genres", string.Join(", ", movie.Genres))
				.AddField("Director", string.Join(", ", movie.Crew.GetDirectors().Take(DisplayLimit).Select(credit => credit.Name)), inline: true)
				.AddField("Screenplay", string.Join(", ", movie.Crew.GetWriters().Take(DisplayLimit).Select(credit => credit.Name)), inline: true)
				.AddField(ZeroLengthSpace, "Cast");

			foreach (var cast in movie.Cast.Take(DisplayLimit))
			{
				embedBuilder.AddField(cast.Role, cast.Name, inline: true);
			}

			embedBuilder.AddField("Links", $"[IMDB]({movie.ImdbUri.ToString()}) [TMDB]({movie.TmdbUri.ToString()})");

			return embedBuilder.Build();
		}
	}
}