namespace MovieStoreCore.Domain
{
    public class PurchasedMovie
    {
        public Guid Id { get; set; }
        public Movie Movie { get; set; } = new();
        public Customer Customer { get; set; } = new();
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
