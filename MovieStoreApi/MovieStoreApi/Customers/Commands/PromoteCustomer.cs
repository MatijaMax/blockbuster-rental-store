using MediatR;
using MovieStoreCore.Domain;
using MovieStoreCore.Domain.Enums;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class PromoteCustomer
    {
        public class Query : IRequest<Customer?>
        {
            public Guid Id { get; set; }
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
                Customer? oldCustomer = _repository.GetByID(request.Id);
                if (_repository.GetByID(request.Id) == null)
                {
                    return Task.FromResult<Customer?>(null);
                }

                //TODO 
                //ADVANCED USLOVI PROMOCIJE
                //NIJE MU ISTEKLO
                //2. poslednjih x meseci je kupio y filmova
                //3. kad uvedemo cenu da je potrosio x kolicinu novca
                oldCustomer.StatusExpirationDate = DateTime.Now.AddYears(1);
                oldCustomer.Status = Status.Advanced;

                return Task.FromResult(oldCustomer);
            }
        }
    }
}
