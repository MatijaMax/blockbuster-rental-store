using Microsoft.EntityFrameworkCore;
using MovieStoreCore.Domain;

namespace MovieStoreInfrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(MovieStoreContext context) : base(context)
        {
        }

        public override IEnumerable<Customer> GetAll() =>
            context.Customers.Include(c => c.PurchasedMovies).ThenInclude(pm => pm.Movie).ToList();
    }
}
