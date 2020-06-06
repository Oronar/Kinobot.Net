using AutoMapper;
using Kinobot.Net.Models;
using System;
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
				.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(genre => genre.Name)))
				.ForMember(dest => dest.RunTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes((double)src.Runtime)))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.VoteAverage));

			CreateMap<SearchMovie, Movie>();
		}
	}
}