using Microsoft.EntityFrameworkCore;
using ShowsWebApp.Server.Data;
using ShowsWebApp.Server.Models;

namespace ShowsWebApp.Server.Repositories.ShowRepos
{
    public class ShowRepository : Repository<Show>, IShowRepository
    {
        public ShowRepository(ShowsWebAppServerContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Show>> GetShowsFull()
        {
            var shows = await _context.Shows
                .Include(s => s.Seasons)
                .ThenInclude(s => s.Episodes)
                .Select(s => new Show
                {
                    Id = s.Id,
                    Title = s.Title,
                    Genre = s.Genre,
                    Language = s.Language,
                    Description = s.Description,
                    Seasons = s.Seasons.Select(season => new Season
                    {
                        Id = season.Id,
                        Title = season.Title,
                        ShowId = season.ShowId,
                        Episodes = season.Episodes.Select(episode => new Episode
                        {
                            Id = episode.Id,
                            Title = episode.Title,
                            SeasonId = episode.SeasonId,
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
            return shows;
        }
    }
}
