using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EasyCommerce.Models;

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        // Sample in-memory data
        private static List<Customer> Customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Customer 1", Email = "customer1@example.com" },
            new Customer { Id = 2, Name = "Customer 2", Email = "customer2@example.com" }
        };

        // GET: api/Customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(Customers);
        }

        // GET: api/Customer/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = Customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public ActionResult<Customer> Create(Customer newCustomer)
        {
            newCustomer.Id = Customers.Count + 1; // Assigning an ID (simple approach for in-memory data)
            Customers.Add(newCustomer);
            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        // PUT: api/Customer/{id}
        [HttpPut("{id}")]
        public ActionResult<Customer> Update(int id, Customer updatedCustomer)
        {
            var existingCustomer = Customers.Find(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update properties
            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;

            return Ok(existingCustomer);
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var customerToRemove = Customers.Find(c => c.Id == id);
            if (customerToRemove == null)
            {
                return NotFound();
            }

            Customers.Remove(customerToRemove);
            return NoContent();
        }
    }
}
