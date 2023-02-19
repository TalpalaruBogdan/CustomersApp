using Microsoft.EntityFrameworkCore;
using CustomerService.Data;
using CustomerService.Models;
using CustomerService.DTOs;

namespace CustomerService.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;

        public CustomerRepository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers;
        }

        public async Task<Customer?> GetCustomer(Guid id)
        {
            return await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer?> GetCustomer(string email)
        {
            return await context.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Guid> AddCustomer(Customer customer)
        {
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            return customer.Id;
        }

        public async Task DeleteCustomer(Customer customer)
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Guid id, UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await GetCustomer(id);
            if (updateCustomerDto.Address != string.Empty)
                existingCustomer!.Address = updateCustomerDto.Address;
            if (updateCustomerDto.Country != string.Empty)
                existingCustomer!.Country = updateCustomerDto.Country;
            if (updateCustomerDto.Email != string.Empty)
                existingCustomer!.Email = updateCustomerDto.Email;
            await context.SaveChangesAsync();
        }
    }
}
