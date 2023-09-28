using MovieStoreCore.Domain.Enums;

namespace MovieStoreCore.Domain
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public LicensingType LicensingType { get; set; }

        public double Price { get; set; }
    }
}
