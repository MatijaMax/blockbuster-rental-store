using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Queries
{
    public static class CreateCustomer
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
                if (_repository.GetByID(request.newCustomer.Id) != null)
                {
                    return Task.FromResult<Customer?>(null);
                }
                _repository.Insert(request.newCustomer);
                return Task.FromResult(request.newCustomer);
            }
        }
    }
}
