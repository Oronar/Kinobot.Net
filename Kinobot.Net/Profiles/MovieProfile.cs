using AutoMapper;
using Kinobot.Net.Models;
using TMDbLib.Objects.Search;

namespace Kinobot.Net.Profiles
{
	public class MovieProfile : Profile
	{
		public MovieProfile()
		{
			CreateMap<TMDbLib.Objects.Movies.Movie, Movie>();

			CreateMap<SearchMovie, Movie>();

		}
	}
}