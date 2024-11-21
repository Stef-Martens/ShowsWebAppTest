using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Models;

namespace ShowsWebApp.Server.Services
{
    public interface ISeasonService : IService<Season, SeasonDTO>
    {
        // goed voor toevoegen van extra methodes
        // bijvoorbeeld:
        // Task<IEnumerable<Season>> GetSeasonsFull();
    }
}
