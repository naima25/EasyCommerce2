using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EasyCommerce.Controllers;
using EasyCommerce.Models;
using EasyCommerce.Data;
using EasyCommerce.Services;
using Xunit;
using System;
using System.Threading.Tasks;

namespace EasyCommerce.Tests
{
    public class UpdateOrderTests
    {
        [Fact]
        public async Task UpdateOrder_ReturnsNoContent()
        {
            // Setup In-Memory Database
            var options = new DbContextOptionsBuilder<EasyCommerceContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            using var context = new EasyCommerceContext(options);
            var orderService = new OrderService(context);  // Service for interacting with the DB

            // Create a logger
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<OrderController>();

            // Create the controller with the order service and logger
            var controller = new OrderController(orderService, logger);

            // Prepare a sample order for testing
            var order = new Order { OrderDate = DateTime.Now, TotalAmount = 100.0m };

            //Create the order first to ensure it exists
            await controller.CreateOrder(order);

            // Now modify the order (e.g., update the total amount)
            order.TotalAmount = 150.0m;

            //Call the UpdateOrder method
            var result = await controller.UpdateOrder(order.Id, order);

            //Verify that the result is a NoContentResult (indicating the update was successful)
            var actionResult = Assert.IsType<NoContentResult>(result);
        }
    }
}
