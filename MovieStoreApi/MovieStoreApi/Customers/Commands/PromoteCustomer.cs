using MediatR;
using MovieStoreCore.Domain;
using MovieStoreCore.Domain.Enums;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class PromoteCustomer
    {
        public class Query : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, bool>
        {
            private readonly IRepository<Customer> _customerRepository;

            public RequestHandler(IRepository<Customer> customerRepository)
            {
                _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            }

            public Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                Customer? customer = _customerRepository.GetByID(request.Id);
                if (customer == null)
                {
                    return Task.FromResult(false);
                }
                //TODO 
                //Conditionals
                //1.Check status piration date
                //2.Has the user bought enough(x) movies in the last(y) month
                //3.Has he spent an x amount of money
                if (IsStatusExpired(customer) && IsPurchaseAmountSatisfied(customer) && IsEnoughMoneySpent())
                {
                    customer.StatusExpirationDate = DateTime.Now.AddYears(1);
                    customer.Status = Status.Advanced;
                }
                _customerRepository.Save();
                return Task.FromResult(true);
            }

            private static bool IsStatusExpired(Customer customer)
            {
                return customer.Status != Status.Advanced || !customer.StatusExpirationDate.HasValue || customer.StatusExpirationDate.Value < DateTime.Now;
            }

            //The user must buy at least 2 movies in a time period of 2 months 
            private static bool IsPurchaseAmountSatisfied(Customer customer)
            {
                var purchasedMovies = customer.PurchasedMovies.Where(purchasedMovie => purchasedMovie.PurchaseDate > DateTime.Now.AddMonths(-2));
                return purchasedMovies.Count() > 2;

            }
            public bool IsEnoughMoneySpent()
            {
                //we made everything free
                return true;
            }
        }
    }
}
