using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class UpdateCustomer
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string Email { get; set; } = string.Empty;
        }

        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                Customer? updatedCustomer = _repository.GetByID(request.Id);
                if (updatedCustomer == null)
                {
                    return Task.FromResult(false);
                }
                updatedCustomer.Email = request.Email;
                _repository.Save();
                return Task.FromResult(true);
            }
        }
    }
}
