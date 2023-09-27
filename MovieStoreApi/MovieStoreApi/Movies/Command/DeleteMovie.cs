using MediatR;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Movies.Command
{
    public static class DeleteMovie
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Movie> _repository;

            public RequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                Movie? movie = _repository.GetByID(request.Id);
                if (movie == null)
                {
                    return Task.FromResult(false);
                }
                _repository.Delete(movie);
                _repository.Save();
                return Task.FromResult(true);


            }
        }
    }
}
