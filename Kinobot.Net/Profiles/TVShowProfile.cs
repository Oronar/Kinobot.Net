using AutoMapper;
using Kinobot.Net.Models;
using System.Linq;
using TMDbLib.Objects.Search;

namespace Kinobot.Net.Profiles
{
	public class TVShowProfile : Profile
	{
		public TVShowProfile()
		{
			CreateMap<TMDbLib.Objects.TvShows.TvShow, TVShow>()
				.ForMember(dest => dest.TmdbId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Overview))
				.ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.ExternalIds.ImdbId))
				.ForMember(dest => dest.Creators, opt => opt.MapFrom(src => src.CreatedBy))
				.ForMember(dest => dest.Crew, opt => opt.MapFrom(src => src.Credits.Crew))
				.ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.Credits.Cast))
				.ForMember(dest => dest.Seasons, opt => opt.MapFrom(src => src.Seasons))
				.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name)))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.VoteAverage))
				.ForMember(dest => dest.ImageUri, opt => opt.Ignore());

			CreateMap<SearchTv, TVShow>()
				.ForMember(dest => dest.TmdbId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(Src => Src.Overview))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.VoteAverage))
				.ForMember(dest => dest.ImdbId, opt => opt.Ignore())
				.ForMember(dest => dest.Genres, opt => opt.Ignore())
				.ForMember(dest => dest.Creators, opt => opt.Ignore())
				.ForMember(dest => dest.Crew, opt => opt.Ignore())
				.ForMember(dest => dest.Cast, opt => opt.Ignore())
				.ForMember(dest => dest.Seasons, opt => opt.Ignore())
				.ForMember(dest => dest.ImageUri, opt => opt.Ignore());
		}
	}
}