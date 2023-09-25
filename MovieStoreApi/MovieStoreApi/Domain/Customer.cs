using MovieStoreApi.Domain.Enums;

namespace MovieStoreApi.Domain
{

    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Customer(string customerId, string name, Status status, Role role, DateTime expirationDate)
        {
            CustomerId = customerId;
            Name = name;
            Status = status;
            Role = role;
            ExpirationDate = expirationDate;
        }
    }
}
