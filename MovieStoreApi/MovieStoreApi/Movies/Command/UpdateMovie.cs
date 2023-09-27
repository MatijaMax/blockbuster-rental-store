using MediatR;
using MovieStoreCore.Domain;
using MovieStoreCore.Domain.Enums;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Movies.Command
{
    public static class UpdateMovie
    {
        public class Command : IRequest<Movie?>
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public int Year { get; set; }
            public LicensingType LicensingType { get; set; }
        }

        public class RequestHandler : IRequestHandler<Command, Movie?>
        {
            private readonly IRepository<Movie> _repository;

            public RequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Movie?> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                Movie? updatedMovie = _repository.GetByID(request.Id);

                updatedMovie.Title = request.Title;
                updatedMovie.Year = request.Year;
                updatedMovie.LicensingType = request.LicensingType;
                _repository.Save();
                return Task.FromResult(updatedMovie);

            }
        }
    }
}
