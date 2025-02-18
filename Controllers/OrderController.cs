using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EasyCommerce.Models;

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        // Sample in-memory data
        private static List<Order> Orders = new List<Order>
        {
            new Order { Id = 1, OrderDate = DateTime.Now, CustomerId = 1, TotalAmount = 50.99m },
            new Order { Id = 2, OrderDate = DateTime.Now, CustomerId = 2, TotalAmount = 120.49m }
        };

        // GET: api/Order
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(Orders);
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = Orders.Find(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
