using MovieStoreApi.Domain.Enums;

namespace MovieStoreApi.Domain
{

    public class Movie
    {

        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }
        public LicensingType LicensingType { get; set;}

        public Movie(string title, int year, string director, double rating, LicensingType licensingType)
        {
            Title = title;
            Year = year;
            Director = director;
            Rating = rating;
            LicensingType = licensingType;
        }
    }
}
