using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;
using Scalar.AspNetCore;

namespace MovieManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GenresController : ControllerBase
    {
        private readonly IGenericService<GenreModel> _genreService;

        public GenresController(IGenericService<GenreModel> genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenreModel>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var genres = await _genreService.GetAllAsync(cancellationToken);
            return Ok(genres);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<GenreModel>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var genre = await _genreService.GetByIdAsync(id, cancellationToken);
            if (genre == null)
            {
                return NotFound($"Genre with id {id} not found.");
            }
            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreModel>> CreateAsync(
            [FromBody] GenreModel model,
            CancellationToken cancellationToken = default)
        {
            var created = await _genreService.CreateAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, new { Message = "Genre created successfully.", Data = created });
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateAsync(
            int id,
            [FromBody] GenreModel model,
            CancellationToken cancellationToken = default)
        {
            if (model.Id != id)
            {
                return BadRequest($"Route id ({id}) and body id ({model.Id}) must match.");
            }
            var updated = await _genreService.UpdateAsync(model, cancellationToken);
            if (!updated)
            {
                return NotFound($"Genre with id {id} not found.");
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <IActionResult> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var deleted = await _genreService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound($"Genre with id {id} not found.");
            }
            return NoContent();
        }

    }
}
