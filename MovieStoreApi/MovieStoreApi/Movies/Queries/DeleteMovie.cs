using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Movies.Queries
{
    public static class DeleteMovie
    {
        public class Query : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, bool>
        {
            private readonly IRepository<Movie> _repository;

            public RequestHandler(IRepository<Movie> repository)
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
