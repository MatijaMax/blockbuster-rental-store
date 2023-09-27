using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class PurchaseMovie
    {
        public class Query : IRequest<Customer?>
        {
            public Customer? Customer { get; set; }
            public Movie? Movie { get; set; }
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

                //NE MOZES KUPITI ISTI FILM 2 PUTA 
                return Task.FromResult(request.Customer);
            }
        }
    }
}
