using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieStoreApi.Customers.Queries;
using MovieStoreCore.Domain;

namespace MovieStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly List<Customer> _customers = new List<Customer>();
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _mediator.Send(new GetAllCustomers.Query { });
            return customers.IsNullOrEmpty() ? NotFound() : Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomer.Query { Id = id });
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            var createdCustomer = await _mediator.Send(new CreateCustomer.Query { newCustomer = customer });
            return createdCustomer == null ? NoContent() : Ok(createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, Customer customer)
        {
            var createdCustomer = await _mediator.Send(new UpdateCustomer.Query { newCustomer = customer });
            return createdCustomer == null ? NotFound() : Ok(customer);
        }

        [HttpPut("promote/{id}")]
        public async Task<IActionResult> PromoteCustomer(Guid id)
        {
            var createdCustomer = await _mediator.Send(new PromoteCustomer.Query { Id = id });
            return createdCustomer == null ? NotFound() : Ok(createdCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            bool isFound = await _mediator.Send(new DeleteCustomer.Query { Id = id });
            return isFound == false ? NotFound() : Ok(true);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseMovie(Customer customer, [FromBody] Movie movie)
        {
            PurchasedMovie purchasedMovie = new PurchasedMovie
            {
                Id = Guid.NewGuid(),
                PurchaseDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1),
                Customer = customer,
                Movie = movie
            };
            var purchaser = await _mediator.Send(new PurchaseMovie.Query { Customer = customer, Movie = movie });
            return purchaser == null ? NotFound() : Ok(true);
        }
    }
}


