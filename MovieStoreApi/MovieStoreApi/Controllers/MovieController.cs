using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Movies.Command;
using MovieStoreApi.Movies.Queries;
using MovieStoreCore.Domain;

namespace MovieStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovieController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _mediator.Send(new GetAllMovies.Query { });
            return Ok(movies);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            var movie = await _mediator.Send(new GetMovie.Query { Id = id });
            return movie == null ? NotFound() : Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMovie(CreateMovie.Command command)
        {
            var createdMovie = await _mediator.Send(command);
            return Ok(createdMovie);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovie(UpdateMovie.Command command)
        {
            var isFound = await _mediator.Send(command);
            return isFound ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            bool isFound = await _mediator.Send(new DeleteMovie.Command { Id = id });
            return isFound ? Ok() : NotFound();
        }
    }
}

