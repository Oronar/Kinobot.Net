using Kinobot.Net.Extensions;
using Kinobot.Net.Models;
using System.Collections.Generic;
using Xunit;

namespace Kinobot.Net.Tests.Extensions
{
	public class CreditExtensionsTests
	{
		[Fact]
		public void GetRoles_WithMatchingRoles_ReturnsRoles()
		{
			var credits = new List<Credit>() { new Credit() { Role = "Match" } };

			var result = credits.GetRoles(new List<string>() { "Match" });

			Assert.Single(result);
		}

		[Fact]
		public void GetRoles_WithNoMatchingRoles_ReturnsEmpty()
		{
			var credits = new List<Credit>() { new Credit() { Role = "NoMatch" } };

			var result = credits.GetRoles(new List<string>() { "Match" });

			Assert.Empty(result);
		}

		[Fact]
		public void GetDirectors_WithDirectors_ReturnsDirectors()
		{
			var credits = new List<Credit>() { new Credit() { Role = "Director" } };

			var result = credits.GetDirectors();

			Assert.Single(result);
		}

		[Fact]
		public void GetDirectors_WithNoDiretors_ReturnsEmpty()
		{
			var credits = new List<Credit>() { new Credit() { Role = "NoMatch" } };

			var result = credits.GetDirectors();

			Assert.Empty(result);
		}

		[Fact]
		public void GetWriters_WithWriter_ReturnsWriters()
		{
			var credits = new List<Credit>() { new Credit() { Role = "Writer" } };

			var result = credits.GetWriters();

			Assert.Single(result);
		}

		[Fact]
		public void GetWriters_WithScreenplay_ReturnsWriters()
		{
			var credits = new List<Credit>() { new Credit() { Role = "Screenplay" } };

			var result = credits.GetWriters();

			Assert.Single(result);
		}

		[Fact]
		public void GetWriters_WithNoWriters_ReturnsEmpty()
		{
			var credits = new List<Credit>() { new Credit() { Role = "NoMatch" } };

			var result = credits.GetWriters();

			Assert.Empty(result);
		}

		[Fact]
		public void GetCreators_WithCreator_ReturnsCreators()
		{
			var credits = new List<Credit>() { new Credit() { Role = "Producer" } };

			var result = credits.GetProducers();

			Assert.Single(result);
		}

		[Fact]
		public void GetCreators_WithNoCreators_ReturnsEmpty()
		{
			var credits = new List<Credit>() { new Credit() { Role = "NoMatch" } };

			var result = credits.GetProducers();

			Assert.Empty(result);
		}
	}
}