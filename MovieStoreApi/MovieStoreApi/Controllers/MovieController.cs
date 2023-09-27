using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Movies.Command;
using MovieStoreApi.Movies.Queries;
using MovieStoreCore.Domain;
using MovieStoreCore.Domain.Enums;

namespace MovieStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovieController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            List<Movie> movies = await _mediator.Send(new GetAllMovies.Query { });
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            var movie = await _mediator.Send(new GetMovie.Query { Id = id });
            return movie == null ? NotFound() : Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(string title, int year, string licensingType)
        {
            //validation
            LicensingType validatedLicensingType;
            if (Enum.TryParse<LicensingType>(licensingType, true, out validatedLicensingType)!)
            {
                return BadRequest();
            }
            var createdMovie = await _mediator.Send(new CreateMovie.Command { Title = title, Year = year, LicensingType = validatedLicensingType }); ;
            return Ok(createdMovie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(Guid id, string title, int year, string licensingType)
        {
            LicensingType validatedLicensingType;
            if (Enum.TryParse<LicensingType>(licensingType, true, out validatedLicensingType)!)
            {
                return BadRequest();
            }
            var updatedMovie = await _mediator.Send(new UpdateMovie.Command { Id = id, Title = title, Year = year, LicensingType = validatedLicensingType });
            return Ok(updatedMovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            bool isFound = await _mediator.Send(new DeleteMovie.Command { Id = id });
            return isFound == false ? NotFound() : Ok(true);
        }
    }
}

