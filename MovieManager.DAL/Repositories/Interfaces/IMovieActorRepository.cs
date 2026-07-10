using MovieManager.DAL.Entities;

namespace MovieManager.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the MovieActorRepository, which provides methods to manage the relationship between movies and actors in the data access layer.
    /// </summary>
    public interface IMovieActorRepository
    {
        Task<MovieActor?> GetByIdsAsync(int movieId, int actorId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<MovieActor>> GetByMovieIdAsync(int movieId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int movieId, int actorId, CancellationToken cancellationToken = default);
        Task AddAsync(MovieActor movieActor, CancellationToken cancellationToken = default);
        void Update(MovieActor movieActor);
        void Remove(MovieActor movieActor);
    }
}
