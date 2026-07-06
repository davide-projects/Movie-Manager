

namespace MovieManager.DAL.Repositories.Interfaces
{
    /// <summary>
    /// This interface represents a unit of work pattern for managing database transactions and coordinating the work of multiple repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task <int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
