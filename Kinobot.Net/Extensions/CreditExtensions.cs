using Kinobot.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kinobot.Net.Extensions
{
	public static class CreditExtensions
	{
		public static IEnumerable<Credit> GetDirectors(this IEnumerable<Credit> credits)
		{
			return credits.GetRoles(new List<string>() { "Director" });
		}

		public static IEnumerable<Credit> GetWriters(this IEnumerable<Credit> credits)
		{
			return credits.GetRoles(new List<string>() { "Writer", "Screenplay" });
		}

		public static IEnumerable<Credit> GetCreators(this IEnumerable<Credit> credits)
		{
			return credits.GetRoles(new List<string>() { "Creator" });
		}

		public static IEnumerable<Credit> GetRoles(this IEnumerable<Credit> credits, IEnumerable<string> roles)
		{
			return credits.Where(credit => roles.Contains(credit.Role, StringComparer.InvariantCultureIgnoreCase));
		}
	}
}