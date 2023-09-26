using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Movies.Queries
{
    public static class GetMovie
    {
        public class Query : IRequest<Movie?>
        {
            public Guid Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, Movie?>
        {
            private readonly IRepository<Movie> _repository;

            public RequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Movie?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var movie = _repository.GetByID(request.Id);

                return Task.FromResult(movie);
            }
        }
    }
}