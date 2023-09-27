using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Movies.Command;
using MovieStoreApi.Movies.Queries;

namespace MovieStoreApi.Controllers
{
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
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _mediator.Send(new GetAllMovies.Query { });
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            var movie = await _mediator.Send(new GetMovie.Query { Id = id });
            return movie == null ? NotFound() : Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovie.Command command)
        {
            var createdMovie = await _mediator.Send(command);
            return Ok(createdMovie);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateMovie(UpdateMovie.Command command)
        {
            var isFound = await _mediator.Send(command);
            return isFound ? Ok(true) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            bool isFound = await _mediator.Send(new DeleteMovie.Command { Id = id });
            return isFound ? Ok(true) : NotFound();
        }
    }
}

