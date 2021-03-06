﻿using System;
using System.Collections.Generic;

namespace Kinobot.Net.Models
{
	public class Movie : Media
	{
		public override MediaType MediaType { get { return MediaType.Movie; } }
		public int TmdbId { get; set; }
		public string ImdbId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Uri ImageUri { get; set; }
		public Uri ImdbUri { get { return new Uri($"https://www.imdb.com/title/{ImdbId}"); } }
		public Uri TmdbUri { get { return new Uri($"https://www.themoviedb.org/movie/{TmdbId}"); } }
		public DateTime ReleaseDate { get; set; }
		public IEnumerable<string> Genres { get; set; }
		public TimeSpan RunTime { get; set; }
		public double Rating { get; set; }
		public decimal Budget { get; set; }
		public decimal Revenue { get; set; }
		public IEnumerable<Credit> Crew { get; set; }
		public IEnumerable<Credit> Cast { get; set; }
	}
}