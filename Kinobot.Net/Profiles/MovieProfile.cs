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
				.ForMember(dest => dest.TmdbId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Overview))
				.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(genre => genre.Name)))
				.ForMember(dest => dest.RunTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes((double)src.Runtime)))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.VoteAverage))
				.ForMember(dest => dest.Crew, opt => opt.MapFrom(src => src.Credits.Crew))
				.ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.Credits.Cast));

			CreateMap<SearchMovie, Movie>();
		}
	}
}