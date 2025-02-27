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
    public class DeleteOrderTests
    {
        [Fact]
        public async Task DeleteOrder_ReturnsNoContent()
        {
            // Setup In-Memory Database
            var options = new DbContextOptionsBuilder<EasyCommerceContext>()
                .UseInMemoryDatabase("TestDB")  // Using an in-memory database for testing
                .Options;

            using var context = new EasyCommerceContext(options);
            var orderService = new OrderService(context);  // Service for interacting with the DB

            // Create a logger
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<OrderController>();

            // Create the controller with the order service and logger
            var controller = new OrderController(orderService, logger);

            // Prepare a sample order for testing
            var order = new Order { OrderDate = DateTime.Now, TotalAmount = 100.0m };

            // Act: Create the order first to ensure it exists
            await controller.CreateOrder(order);

            // Act: Now call the DeleteOrder method
            var result = await controller.DeleteOrder(order.Id);

            // Assert: Verify the result is a NoContentResult (this means the order was deleted)
            var actionResult = Assert.IsType<NoContentResult>(result);  // Directly check for NoContentResult
        }
    }
}
