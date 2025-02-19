using EasyCommerce.Data;  // Add the namespace for EasyCommerceContext
using EasyCommerce.Models; // Your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For async database methods

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly EasyCommerceContext _context; // DbContext for SQLite

        public CustomerController(EasyCommerceContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            // Get all customers from the database
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // GET: api/Customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            // Find a customer by ID from the database
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer newCustomer)
        {
            // Add a new customer to the database
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return the created customer
            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        // PUT: api/Customer/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(int id, Customer updatedCustomer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update the customer properties
            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingCustomer);
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var customerToRemove = await _context.Customers.FindAsync(id);
            if (customerToRemove == null)
            {
                return NotFound();
            }

            // Remove the customer from the database
            _context.Customers.Remove(customerToRemove);
            await _context.SaveChangesAsync(); // Save the changes to the database

            return NoContent(); // Return success
        }
    }
}
