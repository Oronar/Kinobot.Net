using AutoMapper;
using Kinobot.Net.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Kinobot.Net.Profiles
{
	public class SearchPageProfile : Profile
	{
		public SearchPageProfile()
		{
			CreateMap<SearchContainer<SearchMovie>, SearchPage<Movie>>()
				.ForMember(dest => dest.PageTotal, opt => opt.MapFrom(src => src.TotalPages));

			CreateMap<SearchContainer<SearchTv>, SearchPage<TVShow>>()
				.ForMember(dest => dest.PageTotal, opt => opt.MapFrom(src => src.TotalPages));
		}
	}
}