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
    }
}
