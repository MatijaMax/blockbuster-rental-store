using MovieStoreCore.Domain.Enums;

namespace MovieStoreApi.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
        public DateTime? StatusExpirationDate { get; set; }
    }
}
