using ShowsWebApp.Server.Models;

namespace ShowsWebApp.Server.Repositories.ShowRepos
{
    public interface IShowRepository : IRepository<Show>
    {
        // enkel nodig voor specifieke methodes voor de show repository
        // bv
        Task<IEnumerable<Show>> GetShowsFull();
    }
}
