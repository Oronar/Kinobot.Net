using AutoMapper;
using Kinobot.Net.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Kinobot.Net.Profiles
{
	public class CreditProfile : Profile
	{
		public CreditProfile()
		{
			CreateMap<Cast, Credit>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Character));

			CreateMap<Crew, Credit>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Job));
		}
	}
}