using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Models;

namespace ShowsWebApp.Server.Services
{
    public interface IShowService : IService<Show, ShowDTO>
    {
        // goed voor toevoegen van extra methodes
        // bijvoorbeeld:
        Task<IEnumerable<Show>> GetShowsFull();
    }
}
