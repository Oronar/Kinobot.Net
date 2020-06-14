using System;

namespace Kinobot.Net.Models
{
	public class Episode
	{
		public string Title { get; set; }
		public int Season { get; set; }
		public int Number { get; set; }
		public DateTime AirDate { get; set; }
	}
}