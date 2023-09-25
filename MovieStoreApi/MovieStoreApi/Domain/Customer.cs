using MovieStoreApi.Domain.Enums;

namespace MovieStoreApi.Domain
{

    public class Customer
    {

        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Customer(string customerId, string firstName, string lastName, DateTime birthDate, string email, Status status, Role role, DateTime expirationDate)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            Status = status;
            Role = role;
            ExpirationDate = expirationDate;
        }
    }
}
