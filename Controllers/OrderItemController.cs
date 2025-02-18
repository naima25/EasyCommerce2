using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EasyCommerce.Models;

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        // Sample in-memory data
        private static List<OrderItem> OrderItems = new List<OrderItem>
        {
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, Price = 10.99m },
            new OrderItem { Id = 2, OrderId = 2, ProductId = 2, Quantity = 1, Price = 20.99m }
        };

        // GET: api/OrderItem
        [HttpGet]
        public ActionResult<IEnumerable<OrderItem>> Get()
        {
            return Ok(OrderItems);
        }

        // GET: api/OrderItem/{id}
        [HttpGet("{id}")]
        public ActionResult<OrderItem> Get(int id)
        {
            var orderItem = OrderItems.Find(oi => oi.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }
    }
}
