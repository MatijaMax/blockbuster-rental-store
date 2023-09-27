using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class UpdateCustomer
    {
        public class Command : IRequest<Customer?>
        {
            public Guid Id { get; set; }
            public string Email { get; set; } = string.Empty;
        }

        public class RequestHandler : IRequestHandler<Command, Customer?>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Customer?> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                Customer? updatedCustomer = _repository.GetByID(request.Id);
                updatedCustomer.Email = request.Email;

                return Task.FromResult(updatedCustomer);
            }
        }
    }
}
