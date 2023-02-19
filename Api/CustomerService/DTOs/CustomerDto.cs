namespace CustomerService.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
    }
}
