using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieStoreApi.Customers.Queries;
using MovieStoreCore.Domain;
using MovieStoreCore.Domain.Enums;

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

            _customers.Add(customer);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(Guid id, Customer newCustomer)
        {
            Customer? oldCustomer = _customers.Find(c => c.Id == id);
            if (oldCustomer == null)
            {
                return NotFound();
            }
            oldCustomer.StatusExpirationDate = newCustomer.StatusExpirationDate;
            oldCustomer.Status = newCustomer.Status;
            oldCustomer.Email = newCustomer.Email;
            oldCustomer.Role = newCustomer.Role;
            return NoContent();
        }

        [HttpPut("promote/{id}")]
        public IActionResult PromoteCustomer(Guid id)
        {
            Customer? oldCustomer = _customers.Find(c => c.Id == id);
            if (oldCustomer == null)
            {
                return NotFound();
            }
            oldCustomer.StatusExpirationDate = DateTime.Now.AddYears(1);
            oldCustomer.Status = Status.Advanced;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            Customer? customer = _customers.Find(c => c.Id == id);
            if (customer != null)
            {
                _customers.Remove(customer);
                return NoContent();
            }
            return NotFound();
        }

        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomer.Query { Id = id });
            return customer == null ? NotFound() : Ok(customer);
        }


        [HttpPost("purchase")]
        public IActionResult PurchaseMovie(Customer customer, [FromBody] Movie movie)
        {
            PurchasedMovie purchasedMovie = new PurchasedMovie
            {
                Id = Guid.NewGuid(),
                PurchaseDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1),
                Customer = customer,
                Movie = movie
            };

            // Process the purchasedMovie object as needed
            return NoContent();
        }
    }
}


