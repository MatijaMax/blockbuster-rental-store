using MediatR;
using MovieStoreCore.Domain;
using MovieStoreCore.Domain.Enums;
using MovieStoreInfrastructure.Repositories;

namespace MovieStoreApi.Customers.Commands
{
    public static class PurchaseMovie
    {
        public class Command : IRequest<bool>
        {
            public Guid CustomerId { get; set; }
            public Guid MovieId { get; set; }

        }

        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _customerRepository;
            private readonly IRepository<Movie> _movieRepository;
            private readonly IRepository<PurchasedMovie> _purchasedMovieRepository;

            public RequestHandler(IRepository<Customer> customerRepository, IRepository<Movie> movieRepository, IRepository<PurchasedMovie> purchasedMovieRepository)
            {
                _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
                _purchasedMovieRepository = purchasedMovieRepository ?? throw new ArgumentNullException(nameof(purchasedMovieRepository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {

                Customer? customer = _customerRepository.GetByID(request.CustomerId);
                Movie? movie = _movieRepository.GetByID(request.MovieId);
                if (customer == null || movie == null)
                {
                    return Task.FromResult(false);
                }

                if (customer.PurchasedMovies.Any(purchasedMovie => purchasedMovie.Movie == movie))
                {
                    return Task.FromResult(false);
                }

                PurchasedMovie purchasedMovie = new PurchasedMovie { Customer = customer, Movie = movie, PurchaseDate = DateTime.Now };
                if (movie.LicensingType == LicensingType.TwoDay)
                {
                    purchasedMovie.ExpirationDate = DateTime.Now.AddDays(2);
                }
                _purchasedMovieRepository.Insert(purchasedMovie);
                _purchasedMovieRepository.Save();
                return Task.FromResult(true);
            }
        }
    }
}
