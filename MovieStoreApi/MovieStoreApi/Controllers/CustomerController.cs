using Microsoft.AspNetCore.Mvc;
using MovieStoreCore.Domain;
using MovieStoreCore.Domain.Enums;

namespace MovieStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly List<Customer> _customers = new List<Customer>();

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(Guid id)
        {
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
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
        public IActionResult DeleteCustomer(Guid id)
        {
            Customer? customer = _customers.Find(c => c.Id == id);
            if (customer != null)
            {
                _customers.Remove(customer);
                return NoContent();
            }
            return NotFound();
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


