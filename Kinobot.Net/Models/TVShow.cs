using System;
using System.Collections.Generic;

namespace Kinobot.Net.Models
{
	public class TVShow : Media
	{
		public override MediaType MediaType { get { return MediaType.TVShow; } }
		public int TmdbId { get; set; }
		public string ImdbId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime FirstAirDate { get; set; }
		public IEnumerable<string> Genres { get; set; }
		public IEnumerable<Credit> Creators { get; set; }
		public IEnumerable<Credit> Crew { get; set; }
		public IEnumerable<Credit> Cast { get; set; }
		public IEnumerable<SeasonSummary> Seasons { get; set; }
		public int Rating { get; set; }
		public Uri ImageUri { get; set; }
		public Uri ImdbUri { get { return new Uri($"https://www.imdb.com/title/{ImdbId}"); } }
		public Uri TmdbUri { get { return new Uri($"https://www.themoviedb.org/tv/{TmdbId}"); } }
	}
}