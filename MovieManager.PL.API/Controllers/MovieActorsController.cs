using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;

namespace MovieManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MovieActorsController : ControllerBase
    {
        private readonly IMovieActorService _movieActorService;

        public MovieActorsController(IMovieActorService movieActorService)
        {
            _movieActorService = movieActorService;
        }

        [HttpGet("movie/{movieId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<MovieActorModel>>> GetByMovieIdAsync(
            int movieId,
            CancellationToken cancellationToken = default)
        {
            var movieActors = await _movieActorService.GetByMovieIdAsync(movieId, cancellationToken);
            return Ok(movieActors);
        }

        [HttpGet("{movieId:int}/{actorId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieActorModel>> GetByIdsAsync(
            int movieId,
            int actorId,
            CancellationToken cancellationToken = default)
        {
            var movieActor = await _movieActorService.GetByIdsAsync(movieId, actorId, cancellationToken);
            if (movieActor == null)
            {
                return NotFound($"MovieActor with movieId {movieId} and actorId {actorId} not found.");
            }
            return Ok(movieActor);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieActorModel>> CreateAsync(
            [FromBody] MovieActorModel model,
            CancellationToken cancellationToken = default)
        {
            var created = await _movieActorService.CreateAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdsAsync),
                new { movieId = created.MovieId, actorId = created.ActorId },
                new { Message = "MovieActor created successfully.", Data = created });
        }

        [HttpPut("{movieId:int}/{actorId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(
            int movieId,
            int actorId,
            [FromBody] MovieActorModel model,
            CancellationToken cancellationToken = default)
        {
            if (model.MovieId != movieId || model.ActorId != actorId)
            {
                return BadRequest(
                    $"Route ids ({movieId}/{actorId}) and body ids ({model.MovieId}/{model.ActorId}) must match.");
            }

            var updated = await _movieActorService.UpdateAsync(model, cancellationToken);
            if (!updated)
            {
                return NotFound($"MovieActor with movieId {movieId} and actorId {actorId} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{movieId:int}/{actorId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(
            int movieId,
            int actorId,
            CancellationToken cancellationToken = default)
        {
            var deleted = await _movieActorService.DeleteAsync(movieId, actorId, cancellationToken);
            if (!deleted)
            {
                return NotFound($"MovieActor with movieId {movieId} and actorId {actorId} not found.");
            }

            return NoContent();
        }
    }
}
