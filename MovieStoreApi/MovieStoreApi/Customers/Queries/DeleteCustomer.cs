using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Queries
{
    public static class DeleteCustomer
    {
        public class Query : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, bool>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (_repository.GetByID(request.Id) == null)
                {
                    return Task.FromResult(false);
                }

                _repository.Delete(_repository.GetByID(request.Id));

                return Task.FromResult(true);
            }
        }
    }
}
