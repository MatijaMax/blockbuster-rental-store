using MovieStoreApi.Domain.Enums;

namespace MovieStoreApi.Domain
{

    public class Movie
    {

        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public LicensingType LicensingType { get; set;}

        public Movie(string title, int year, string director, double rating, double price, LicensingType licensingType)
        {
            Title = title;
            Year = year;
            Director = director;
            Rating = rating;
            Price = price;
            LicensingType = licensingType;
        }
    }
}
