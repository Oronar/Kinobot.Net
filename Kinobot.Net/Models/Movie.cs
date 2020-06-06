using System;
using System.Collections.Generic;

namespace Kinobot.Net.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public DateTime ReleaseDate { get; set; }
		public IEnumerable<string> Genres { get; set; }
		public TimeSpan RunTime { get; set; }
		public double Rating { get; set; }
	}
}