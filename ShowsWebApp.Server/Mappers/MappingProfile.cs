using AutoMapper;
using ShowsWebApp.Server.Models;
using ShowsWebApp.Server.DTOs;

namespace ShowsWebApp.Server.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Show -> ShowDto and reverse
            CreateMap<Show, ShowDTO>().ReverseMap();
            CreateMap<Season, SeasonSummaryDTO>().ReverseMap();

            // Season -> SeasonDto and reverse
            CreateMap<Season, SeasonDTO>()
                //.ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.Show.Id))
                .ReverseMap();

            // Episode -> EpisodeDto and reverse
            CreateMap<Episode, EpisodeDTO>()
                //.ForMember(dest => dest.SeasonId, opt => opt.MapFrom(src => src.Season.Id))
                .ReverseMap();
        }
    }
}
