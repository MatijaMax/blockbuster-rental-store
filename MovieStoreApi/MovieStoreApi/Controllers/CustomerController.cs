using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Customers.Commands;
using MovieStoreApi.Customers.Queries;
using MovieStoreCore.Domain;

namespace MovieStoreApi.Controllers
{
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
        public async Task<IActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _mediator.Send(new GetAllCustomers.Query { });
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomer.Query { Id = id });
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(string email)
        {
            var createdCustomer = await _mediator.Send(new CreateCustomer.Command { Email = email });
            return Ok(createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, string email)
        {

            var result = await _mediator.Send(new UpdateCustomer.Command { Id = id, Email = email });
            return result ? Ok() : NotFound();

        }

        [HttpPut("{id}/promote")]
        public async Task<IActionResult> PromoteCustomer(Guid id)
        {
            var updatedCustomer = await _mediator.Send(new PromoteCustomer.Query { Id = id });
            return updatedCustomer ? Ok(true) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            bool isFound = await _mediator.Send(new DeleteCustomer.Command { Id = id });
            return isFound ? Ok(true) : NotFound();
        }

        [HttpPost("{customerId}/purchase/{movieId}")]
        public async Task<IActionResult> PurchaseMovie(Guid customerId, Guid movieId)
        {
            var isCreated = await _mediator.Send(new PurchaseMovie.Command { CustomerId = customerId, MovieId = movieId });
            return isCreated ? Ok(true) : NotFound();
        }
    }
}


