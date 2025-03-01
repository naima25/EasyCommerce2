using EasyCommerce.Models;
using EasyCommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

//The CustomerRepository handles all interactions with the database for customer data
//It uses the EasyCommerceContext to connect to the database and perform CRUD operations
//Instead of directly interacting with the database in other parts of the application
//this repository provides a simple interface for adding, updating, retrieving
//and deleting customer records. This makes the application easier to maintain
//and keeps the database logic separated from the rest of the code


namespace EasyCommerce.Repositories
{
    public class CustomerRepository
    {
        private readonly EasyCommerceContext _context;

        public CustomerRepository(EasyCommerceContext context)
        {
            _context = context;
        }

         // Retrieves all customers from the database asynchronously
        public async Task<IEnumerable<Customer>> GetAllAsync() => await _context.Customers.ToListAsync();

        // Retrieves a customer by their unique ID asynchronously
        public async Task<Customer> GetByIdAsync(int id) => await _context.Customers.FindAsync(id);

         // Adds a new customer to the database asynchronously
        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

         // Updates a new customer to the database asynchronously
        public async Task UpdateAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

         // Deletes a new customer to the database asynchronously
        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
