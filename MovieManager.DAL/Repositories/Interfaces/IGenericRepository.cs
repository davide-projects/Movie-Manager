using System.Linq.Expressions;

namespace MovieManager.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Creates a generic repository interface for performing CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangeAsync(CancellationToken cancellationToken);
    }
}