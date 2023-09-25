using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStoreCore.Domain;

namespace MovieStoreInfrastructure.Configuration
{
    public class PurchasedMovieConfiguration : IEntityTypeConfiguration<PurchasedMovie>
    {
        public void Configure(EntityTypeBuilder<PurchasedMovie> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.HasOne(pm => pm.Movie);

            builder.HasOne(pm => pm.Customer)
                .WithMany(c => c.PurchasedMovies);
        }
    }
}
