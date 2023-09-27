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
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            movie.Id = Guid.NewGuid();
            var createdMovie = await _mediator.Send(new CreateMovie.Query { newMovie = movie });
            return createdMovie == null ? NoContent() : Ok(createdMovie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(Guid id, Movie movie)
        {
            var createdMovie = await _mediator.Send(new UpdateMovie.Query { newMovie = movie });
            return createdMovie == null ? NotFound() : Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            bool isFound = await _mediator.Send(new DeleteMovie.Query { Id = id });
            return isFound == false ? NotFound() : Ok(true);
        }
    }
}

