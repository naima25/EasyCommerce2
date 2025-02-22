using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCommerce.Data;  // Add the namespace for EasyCommerceContext
using EasyCommerce.Models; // Your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For async database methods
using Microsoft.Extensions.Logging; // For ILogger

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly EasyCommerceContext _context; // DbContext for SQLite
        private readonly ILogger<OrderItemController> _logger;  // Injecting the logger

        public OrderItemController(EasyCommerceContext context, ILogger<OrderItemController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/OrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all order items.");
                var orderItems = await _context.OrderItems.ToListAsync();
                
                if (orderItems == null || !orderItems.Any())
                {
                    _logger.LogWarning("No order items found.");
                    return NotFound("No order items found.");
                }

                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching order items.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/OrderItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> Get(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching order item with ID {id}");
                var orderItem = await _context.OrderItems.FindAsync(id);

                if (orderItem == null)
                {
                    _logger.LogWarning($"Order item with ID {id} not found.");
                    return NotFound($"Order item with ID {id} not found.");
                }

                return Ok(orderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the order item with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // POST: api/OrderItem
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create(OrderItem newOrderItem)
        {
            try
            {
                if (newOrderItem == null)
                {
                    _logger.LogWarning("Received empty order item object.");
                    return BadRequest("Order item data cannot be null.");
                }

                _logger.LogInformation("Creating a new order item.");
                _context.OrderItems.Add(newOrderItem);
                await _context.SaveChangesAsync(); // Save changes to the database

                _logger.LogInformation($"Order item with ID {newOrderItem.Id} created.");
                return CreatedAtAction(nameof(Get), new { id = newOrderItem.Id }, newOrderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // PUT: api/OrderItem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> Update(int id, OrderItem updatedOrderItem)
        {
            try
            {
                _logger.LogInformation($"Updating order item with ID {id}");
                var existingOrderItem = await _context.OrderItems.FindAsync(id);
                if (existingOrderItem == null)
                {
                    _logger.LogWarning($"Order item with ID {id} not found for update.");
                    return NotFound($"Order item with ID {id} not found.");
                }

                // Update the order item properties
                existingOrderItem.OrderId = updatedOrderItem.OrderId;
                existingOrderItem.ProductId = updatedOrderItem.ProductId;
                existingOrderItem.Quantity = updatedOrderItem.Quantity;
                existingOrderItem.Price = updatedOrderItem.Price;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Order item with ID {id} updated.");
                return Ok(existingOrderItem);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Error updating order item due to concurrency issue.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Concurrency issue while updating the order item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the order item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // DELETE: api/OrderItem/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting order item with ID {id}");
                var orderItemToRemove = await _context.OrderItems.FindAsync(id);
                if (orderItemToRemove == null)
                {
                    _logger.LogWarning($"Order item with ID {id} not found for deletion.");
                    return NotFound($"Order item with ID {id} not found.");
                }

                // Remove the order item from the database
                _context.OrderItems.Remove(orderItemToRemove);
                await _context.SaveChangesAsync(); // Save the changes to the database

                _logger.LogInformation($"Order item with ID {id} deleted.");
                return NoContent(); // Return success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the order item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
