using AutoMapper;
using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Models;
using ShowsWebApp.Server.Repositories;

namespace ShowsWebApp.Server.Services
{
    public class SeasonService : Service<Season, SeasonDTO>, ISeasonService
    {
        private readonly IRepository<Season> _seasonRepository;
        private readonly IMapper _mapper;

        public SeasonService(IRepository<Season> seasonRepository, IMapper mapper) : base(seasonRepository, mapper)
        {
            _seasonRepository = seasonRepository;
            _mapper = mapper;
        }
    }
}
