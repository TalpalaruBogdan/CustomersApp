using Microsoft.EntityFrameworkCore;
using CustomerService.Models;

namespace CustomerService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
