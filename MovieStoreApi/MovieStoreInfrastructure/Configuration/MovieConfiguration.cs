using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStoreCore.Domain;

namespace MovieStoreInfrastructure.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id); // Assuming Id is the primary key
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(255); // Set an appropriate max length
        }
    }
}
