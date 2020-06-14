using System;

namespace Kinobot.Net.Models
{
	public class SeasonSummary
	{
		public int TmdbId { get; set; }
		public int SeasonNumber { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int EpisodeCount { get; set; }
		public DateTime AirDate { get; set; }
	}
}