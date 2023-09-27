using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Queries
{
    public static class UpdateCustomer
    {
        public class Query : IRequest<Customer?>
        {
            public Customer? newCustomer { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, Customer?>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Customer?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                Customer? oldCustomer = _repository.GetByID(request.newCustomer.Id);
                if (_repository.GetByID(request.newCustomer.Id) == null)
                {
                    return Task.FromResult<Customer?>(null);
                }
                oldCustomer.StatusExpirationDate = request.newCustomer.StatusExpirationDate;
                oldCustomer.Status = request.newCustomer.Status;
                oldCustomer.Email = request.newCustomer.Email;
                oldCustomer.Role = request.newCustomer.Role;
                return Task.FromResult(oldCustomer);
            }
        }
    }
}
