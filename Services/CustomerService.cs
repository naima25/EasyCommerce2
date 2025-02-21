using EasyCommerce.Models;
using EasyCommerce.Data;
using EasyCommerce.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly EasyCommerceContext _context;

        public CustomerService(EasyCommerceContext context)
        {
            _context = context;
        }

        // Get all customers asynchronously
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // Get a customer by their ID (changed to string as IdentityUser uses string as the default ID type)
        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _context.Customers.FindAsync(id);
        }

        // Add a new customer asynchronously
        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        // Update an existing customer asynchronously (ID changed to string)
        public async Task UpdateCustomerAsync(string id, Customer customer)
        {
            // You should retrieve the customer first, and then apply changes to it
            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return; // Handle customer not found scenario (could throw an exception or return a specific result)
            }

            // Update the properties manually to ensure you are only updating the necessary fields
            existingCustomer.FullName = customer.FullName;
            // Update other properties as needed

            await _context.SaveChangesAsync();
        }

        // Delete a customer asynchronously by ID (ID changed to string)
        public async Task DeleteCustomerAsync(string id)
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
