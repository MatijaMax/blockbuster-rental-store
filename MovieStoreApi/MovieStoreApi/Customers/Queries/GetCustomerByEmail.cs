using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Queries;

public static class GetCustomerByEmail
{
    public class Query : IRequest<Customer?>
    {
        public string? Email { get; set; }
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

            var customer = _repository.Find(c => c.Email == request.Email).SingleOrDefault();
            return Task.FromResult(customer);
        }
    }
}
