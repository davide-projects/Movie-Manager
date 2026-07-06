using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieManager.DAL.Data;
using MovieManager.DAL.Repositories.Interfaces;

namespace MovieManager.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MovieDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MovieDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

            EnsureDbSetIsValid();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        Task IGenericRepository<T>.SaveChangeAsync(CancellationToken cancellationToken)
        {
            return SaveChangeAsync(cancellationToken);
        }

        private async Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while saving changes to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while saving changes to the database.", ex);
            }
        }

        private void EnsureDbSetIsValid()
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException($"DbSet for {typeof(T).Name} is not configured!");
            }

        }
    }
}