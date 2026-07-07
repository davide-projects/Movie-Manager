namespace MovieManager.BLL.Services.Interfaces

{
    public interface IGenericService<TModel, TEntity>
    {
        Task<TModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }

}
