using EasyCommerce.Data;  // Add the namespace for EasyCommerceContext
using EasyCommerce.Models; // Your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For async database methods

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly EasyCommerceContext _context; // DbContext for SQLite

        public OrderController(EasyCommerceContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            // Get all orders from the database
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            // Find an order by ID from the database
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order newOrder)
        {
            // Add a new order to the database
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return the created order
            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }

        // PUT: api/Order/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Update(int id, Order updatedOrder)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update the order properties
            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.CustomerId = updatedOrder.CustomerId;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingOrder);
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orderToRemove = await _context.Orders.FindAsync(id);
            if (orderToRemove == null)
            {
                return NotFound();
            }

            // Remove the order from the database
            _context.Orders.Remove(orderToRemove);
            await _context.SaveChangesAsync(); // Save the changes to the database

            return NoContent(); // Return success
        }
    }
}
