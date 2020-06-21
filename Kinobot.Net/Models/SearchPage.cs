using System.Collections.Generic;

namespace Kinobot.Net.Models
{
	public class SearchPage<T>
	{
		public int Page { get; set; }

		public int PageTotal { get; set; }

		public IEnumerable<T> Results { get; set; }
	}
}