using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EasyCommerce.Models;
using EasyCommerce.Interfaces;

namespace EasyCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;  // Injecting the order service
        private readonly ILogger<OrderController> _logger;  // Injecting the logger

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                _logger.LogInformation("Fetching all orders.");
                var orders = await _orderService.GetAllOrdersAsync();
                if (orders == null || !orders.Any())
                {
                    _logger.LogWarning("No orders found.");
                    return NotFound("No orders found.");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching orders.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching order with ID {id}");
                var order = await _orderService.GetOrderByIdAsync(id);

                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    return NotFound($"Order with ID {id} not found.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the order.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        
        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            try
            {
                if (order == null)
                {
                    _logger.LogWarning("Received empty order object.");
                    return BadRequest("Order data cannot be null.");
                }

                await _orderService.AddOrderAsync(order);
                _logger.LogInformation($"Order with ID {order.Id} created.");

                // Return the order with formatted OrderDate
                order.OrderDate = DateTime.ParseExact(order.OrderDate.ToString("yyyy-MM-dd"), "yyyy-MM-dd", null); 
                return CreatedAtAction("GetOrder", new { id = order.Id }, new { order.Id, order.TotalAmount, OrderDate = order.OrderDate.ToString("dd/MM/yyyy") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }


        // PUT: api/Order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            try
            {
                if (id != order.Id)
                {
                    _logger.LogWarning("Order ID mismatch.");
                    return BadRequest("Order ID mismatch.");
                }

                await _orderService.UpdateOrderAsync(id, order);
                _logger.LogInformation($"Order with ID {id} updated.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _orderService.GetOrderByIdAsync(id) == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found for update.");
                    return NotFound($"Order with ID {id} not found.");
                }
                else
                {
                    _logger.LogError("Error updating order.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the order.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    return NotFound($"Order with ID {id} not found.");
                }

                await _orderService.DeleteOrderAsync(id);
                _logger.LogInformation($"Order with ID {id} deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the order.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
