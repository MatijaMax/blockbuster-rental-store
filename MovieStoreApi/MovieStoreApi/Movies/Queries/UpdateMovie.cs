using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Movies.Queries
{
    public static class UpdateMovie
    {
        public class Query : IRequest<Movie?>
        {
            public Movie? newMovie { get; set; }
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
                Movie? oldMovie = _repository.GetByID(request.newMovie.Id);
                if (_repository.GetByID(request.newMovie.Id) == null)
                {
                    return Task.FromResult<Movie?>(null);
                }
                oldMovie.Title = request.newMovie.Title;
                oldMovie.Year = request.newMovie.Year;
                oldMovie.LicensingType = request.newMovie.LicensingType;

                return Task.FromResult(oldMovie);
            }
        }
    }
}
