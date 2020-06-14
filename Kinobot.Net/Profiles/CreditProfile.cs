using AutoMapper;
using Kinobot.Net.Models;
using TMDbLib.Objects.General;

namespace Kinobot.Net.Profiles
{
	public class CreditProfile : Profile
	{
		public CreditProfile()
		{
			CreateMap<TMDbLib.Objects.Movies.Cast, Credit>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Character));

			CreateMap<TMDbLib.Objects.TvShows.Cast, Credit>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Character));

			CreateMap<Crew, Credit>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Job));

			CreateMap<TMDbLib.Objects.TvShows.CreatedBy, Credit>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Creator"));
		}
	}
}