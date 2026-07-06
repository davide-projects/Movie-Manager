namespace MovieManager.BLL.Services.Interfaces

{
    public interface IGenericService <TModel> where TModel : class
    {
        Task<T?> GetByIdAsync<T>(int id, CancellationToken cancellationToken = default) where T : class;
        Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
