using AutoMapper;
using Kinobot.Net.Models;
using TMDbLib.Objects.Search;

namespace Kinobot.Net.Profiles
{
	public class SeasonSummaryProfile : Profile
	{
		public SeasonSummaryProfile()
		{
			CreateMap<SearchTvSeason, SeasonSummary>()
				.ForMember(dest => dest.TmdbId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Overview));
		}
	}
}