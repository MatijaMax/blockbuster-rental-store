using Microsoft.AspNetCore.Mvc;
using MovieStoreCore.Domain;

namespace MovieStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly List<Movie> _movies = new List<Movie>();

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return Ok(_movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(Guid id)
        {
            var movie = _movies.Find(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
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

