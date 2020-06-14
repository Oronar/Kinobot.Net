using AutoMapper;
using Kinobot.Net.Models;

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
				.ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.ExternalIds.ImdbId));
		}
	}
}