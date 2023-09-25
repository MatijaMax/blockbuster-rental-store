namespace MovieStoreApi.Domain
{
    public class PurchasedMovie
    {
        public Guid Id { get; set; }
        public Movie Movie { get; set; }
        public Customer Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
