using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieStoreApi.Movies.Queries;
using MovieStoreCore.Domain;

namespace MovieStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly List<Movie> _movies = new List<Movie>();
        private readonly IMediator _mediator;
        public MovieController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            List<Movie> movies = await _mediator.Send(new GetAllMovies.Query { });
            return movies.IsNullOrEmpty() ? NotFound() : Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            var movie = await _mediator.Send(new GetMovie.Query { Id = id });
            return movie == null ? NotFound() : Ok(movie);
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            movie.Id = Guid.NewGuid();
            _movies.Add(movie);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(Guid id, [FromBody] Movie newMovie)
        {
            Movie oldMovie = _movies.Find(m => m.Id == id);
            if (oldMovie == null)
            {
                return NotFound();
            }
            oldMovie.Title = newMovie.Title;
            oldMovie.Year = newMovie.Year;
            oldMovie.LicensingType = newMovie.LicensingType;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(Guid id)
        {
            Movie movie = _movies.Find(m => m.Id == id);
            if (movie != null)
            {
                _movies.Remove(movie);
                return NoContent();
            }
            return NotFound();
        }
    }
}

