using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Customers.Commands;
using MovieStoreApi.Customers.Queries;
using MovieStoreCore.Domain;

namespace MovieStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _mediator.Send(new GetAllCustomers.Query { });
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomer.Query { Id = id });
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCustomer(string email)
        {
            var createdCustomer = await _mediator.Send(new CreateCustomer.Command { Email = email });
            return Ok(createdCustomer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(Guid id, string email)
        {

            var result = await _mediator.Send(new UpdateCustomer.Command { Id = id, Email = email });
            return result ? Ok() : NotFound();

        }

        [HttpPut("{id}/promote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PromoteCustomer(Guid id)
        {
            var updatedCustomer = await _mediator.Send(new PromoteCustomer.Query { Id = id });
            return updatedCustomer ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            bool isFound = await _mediator.Send(new DeleteCustomer.Command { Id = id });
            return isFound ? Ok() : NotFound();
        }

        [HttpPost("{customerId}/purchase/{movieId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PurchaseMovie(Guid customerId, Guid movieId)
        {
            var isCreated = await _mediator.Send(new PurchaseMovie.Command { CustomerId = customerId, MovieId = movieId });
            return isCreated ? Ok() : NotFound();
        }
    }
}


