using AutoMapper;
using MovieManager.BLL.Services.Interfaces;
using MovieManager.DAL.Repositories.Interfaces;

namespace MovieManager.BLL.Services
{
    public class GenericService<TEntity, TModel> : IGenericService<TModel>
        where TModel : class, IModelWithId, new()
        where TEntity : class, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.Repository<TEntity>();
        }

        public async Task<TModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
                return null;
            return _mapper.Map<TModel>(entity);
        }

        public async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<TModel>>(entities);
        }

        public async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<TEntity>(model);

            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TModel>(entity);
        }

        public async Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            var existingEntity = await _repository.GetByIdAsync(model.Id, cancellationToken);

            if (existingEntity == null)
                return false;

            _mapper.Map(model, existingEntity);
            _repository.Update(existingEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return false;

            _repository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
