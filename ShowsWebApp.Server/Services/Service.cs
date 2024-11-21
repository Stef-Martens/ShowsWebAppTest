using AutoMapper;
using ShowsWebApp.Server.Repositories;

namespace ShowsWebApp.Server.Services
{
    public class Service<T, DTO> : IService<T, DTO>
        where T : class
        where DTO : class
    {
        private readonly IRepository<T> _repository;
        private readonly IMapper _mapper;

        public Service(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DTO> AddAsync(DTO entity)
        {
            var mappedEntity = _mapper.Map<T>(entity);
            var addedEntity = await _repository.AddAsync(mappedEntity);
            return _mapper.Map<DTO>(addedEntity);
        }

        public async Task<DTO> DeleteAsync(int id)
        {
            var deletedEntity = await _repository.DeleteAsync(id);
            return _mapper.Map<DTO>(deletedEntity);
        }

        public async Task<IEnumerable<DTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DTO>>(entities);
        }

        public async Task<DTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DTO>(entity);
        }

        public async Task<DTO> UpdateAsync(DTO entity)
        {
            var mappedEntity = _mapper.Map<T>(entity);
            var updatedEntity = await _repository.UpdateAsync(mappedEntity);
            return _mapper.Map<DTO>(updatedEntity);
        }

    }
}
