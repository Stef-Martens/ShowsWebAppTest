using AutoMapper;
using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Models;
using ShowsWebApp.Server.Repositories;

namespace ShowsWebApp.Server.Services
{
    public class EpisodeService : Service<Episode, EpisodeDTO>, IEpisodeService
    {
        private readonly IRepository<Episode> _episodeRepository;
        private readonly IMapper _mapper;

        public EpisodeService(IRepository<Episode> episodeRepository, IMapper mapper) : base(episodeRepository, mapper)
        {
            _episodeRepository = episodeRepository;
            _mapper = mapper;
        }
    }
}
