using EasyCommerce.Data;  // Add the namespace for EasyCommerceContext
using EasyCommerce.Models; // Your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For async database methods

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly EasyCommerceContext _context; // DbContext for SQLite

        public OrderItemController(EasyCommerceContext context)
        {
            _context = context;
        }

        // GET: api/OrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> Get()
        {
            // Get all order items from the database
            var orderItems = await _context.OrderItems.ToListAsync();
            return Ok(orderItems);
        }

        // GET: api/OrderItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> Get(int id)
        {
            // Find an order item by ID from the database
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        // POST: api/OrderItem
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create(OrderItem newOrderItem)
        {
            // Add a new order item to the database
            _context.OrderItems.Add(newOrderItem);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return the created order item
            return CreatedAtAction(nameof(Get), new { id = newOrderItem.Id }, newOrderItem);
        }

        // PUT: api/OrderItem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> Update(int id, OrderItem updatedOrderItem)
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(id);
            if (existingOrderItem == null)
            {
                return NotFound();
            }

            // Update the order item properties
            existingOrderItem.OrderId = updatedOrderItem.OrderId;
            existingOrderItem.ProductId = updatedOrderItem.ProductId;
            existingOrderItem.Quantity = updatedOrderItem.Quantity;
            existingOrderItem.Price = updatedOrderItem.Price;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingOrderItem);
        }

        // DELETE: api/OrderItem/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orderItemToRemove = await _context.OrderItems.FindAsync(id);
            if (orderItemToRemove == null)
            {
                return NotFound();
            }

            // Remove the order item from the database
            _context.OrderItems.Remove(orderItemToRemove);
            await _context.SaveChangesAsync(); // Save the changes to the database

            return NoContent(); // Return success
        }
    }
}
