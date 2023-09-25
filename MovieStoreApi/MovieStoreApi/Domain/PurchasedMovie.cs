using MovieStoreApi.Domain.Enums;

namespace MovieStoreApi.Domain
{
    public class PurchasedMovie : Movie
    {
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }


        public PurchasedMovie(string title, int year, string director, double rating, LicensingType licensingType, DateTime purchaseDate, DateTime expirationDate)
            : base(title, year, director, rating, licensingType)
        {
            PurchaseDate = purchaseDate;
            ExpirationDate = expirationDate;
        }
    }
}
