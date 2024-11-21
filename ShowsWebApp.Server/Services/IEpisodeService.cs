using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Models;

namespace ShowsWebApp.Server.Services
{
    public interface IEpisodeService : IService<Episode, EpisodeDTO>
    {
        // goed voor toevoegen van extra methodes
        // bijvoorbeeld:
        // public async Task<IEnumerable<Show>> GetShowsFull()
    }
}
