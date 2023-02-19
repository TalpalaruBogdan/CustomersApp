namespace CustomerService.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
