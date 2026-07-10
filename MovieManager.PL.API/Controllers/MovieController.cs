using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;

namespace MovieManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MovieController : ControllerBase
    {
        private readonly IGenericService<MovieModel> _movieService;

        public MovieController(IGenericService<MovieModel> movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetAllAsync()
        {
            var movies = await _movieService.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieModel>> GetByIdAsync(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        [HttpPost]
        public async Task<ActionResult<MovieModel>> CreateAsync([FromBody] MovieModel model)
        {
            var created = await _movieService.CreateAsync(model);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }
    }
}
