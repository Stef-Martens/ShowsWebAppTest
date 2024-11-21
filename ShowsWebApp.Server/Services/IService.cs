namespace ShowsWebApp.Server.Services
{
    public interface IService<T, TDTO>
        where T : class
        where TDTO : class
    {
        Task<TDTO> AddAsync(TDTO entity);
        Task<TDTO> DeleteAsync(int id);
        Task<IEnumerable<TDTO>> GetAllAsync();
        Task<TDTO> GetByIdAsync(int id);
        Task<TDTO> UpdateAsync(TDTO entity);
    }
}
