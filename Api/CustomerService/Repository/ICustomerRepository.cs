using CustomerService.DTOs;
using CustomerService.Models;

namespace CustomerService.Repository
{
    public interface ICustomerRepository
    {
        Task<Guid> AddCustomer(Customer customer);
        IEnumerable<Customer> GetCustomers();
        public Task<Customer?> GetCustomer(Guid id);
        public Task<Customer?> GetCustomer(string email);
        public Task DeleteCustomer(Customer customer);
        public Task UpdateCustomer(Guid id, UpdateCustomerDto updateCustomerDto)
;
    }
}