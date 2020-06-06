using AutoMapper;
using Kinobot.Net.Models;
using TMDbLib.Objects.Movies;

namespace Kinobot.Net.Profiles
{
	public class CastMemberProfile : Profile
	{
		public CastMemberProfile()
		{
			CreateMap<Cast, CastMember>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Character));
		}
	}
}