using AutoMapper;
using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Models;
using ShowsWebApp.Server.Repositories;
using ShowsWebApp.Server.Repositories.ShowRepos;

namespace ShowsWebApp.Server.Services
{
    public class ShowService : Service<Show, ShowDTO>, IShowService
    {
        private readonly IShowRepository _showRepository;
        private readonly IMapper _mapper;

        public ShowService(IShowRepository showRepository, IMapper mapper) : base(showRepository, mapper)
        {
            _showRepository = showRepository;
            _mapper = mapper;
        }

        // goed voor toevoegen van extra methodes
        // bijvoorbeeld:
        public async Task<IEnumerable<Show>> GetShowsFull()
        {
            var shows = await _showRepository.GetShowsFull();
            return shows;
        }
    }
}
