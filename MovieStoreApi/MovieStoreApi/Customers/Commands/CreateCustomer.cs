using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class CreateCustomer
    {
        public class Command : IRequest<Customer>
        {
            public string Email { get; set; } = string.Empty;
        }

        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var customer = _repository.Find(c => c.Email == request.Email).SingleOrDefault();
                if (customer == null)
                {
                    customer = new Customer { Email = request.Email, Role = MovieStoreCore.Domain.Enums.Role.Regular, Status = MovieStoreCore.Domain.Enums.Status.Regular };
                    _repository.Insert(customer);
                }
                _repository.Save();
                return Task.FromResult(customer);
            }
        }
    }
}

