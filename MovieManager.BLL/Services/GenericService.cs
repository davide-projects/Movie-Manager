using AutoMapper;
using MovieManager.BLL.Services.Interfaces;
using MovieManager.DAL.Repositories.Interfaces;

public class GenericService<TModel, TEntity> : IGenericService<TModel>
    where TModel : class
    where TEntity : class
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

    public Task<TModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
