using AutoMapper;
using Kinobot.Net.Models;

namespace Kinobot.Net.Profiles
{
	public class MovieProfile : Profile
	{
		public MovieProfile()
		{
			CreateMap<TMDbLib.Objects.Movies.Movie, Movie>()
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
		}
	}
}