using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class DeleteCustomer
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
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
                Customer? customer = _repository.GetByID(request.Id);
                if (customer == null)
                {
                    return Task.FromResult(false);
                }
                _repository.Delete(customer);
                _repository.Save();
                return Task.FromResult(true);
            }
        }
    }
}
