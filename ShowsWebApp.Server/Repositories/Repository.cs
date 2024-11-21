
using Microsoft.EntityFrameworkCore;
using ShowsWebApp.Server.Data;

namespace ShowsWebApp.Server.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShowsWebAppServerContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ShowsWebAppServerContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id, string includeProperties)
        {
            return await _dbSet.Include(includeProperties).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
