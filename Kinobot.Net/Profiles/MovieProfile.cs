using AutoMapper;
using Kinobot.Net.Models;
using System.Linq;
using TMDbLib.Objects.Search;

namespace Kinobot.Net.Profiles
{
	public class MovieProfile : Profile
	{
		public MovieProfile()
		{
			CreateMap<TMDbLib.Objects.Movies.Movie, Movie>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Overview))
				.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(genre => genre.Name)));

			CreateMap<SearchMovie, Movie>();
		}
	}
}