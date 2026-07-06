
using AutoMapper;
using MovieManager.BLL.Services.Interfaces;
using MovieManager.DAL.Repositories.Interfaces;

namespace MovieManager.BLL.Services
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel>
    where TModel : class
    where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync<T>(int id, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}