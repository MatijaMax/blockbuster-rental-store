using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Movies.Queries
{
    public static class GetAllMovies
    {
        public class Query : IRequest<List<Movie>>
        {

        }

        public class RequestHandler : IRequestHandler<Query, List<Movie>>
        {
            private readonly IRepository<Movie> _repository;

            public RequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<List<Movie>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                List<Movie> movies = _repository.GetAll().ToList();

                return Task.FromResult(movies);
            }
        }
    }
}
