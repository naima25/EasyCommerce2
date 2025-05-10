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
            var controller = new OrderController(context, logger);

            // Prepare a sample order for testing
            var order = new Order { OrderDate = DateTime.Now, Price = 100.0m };  // Use Price instead of TotalAmount

            // Create the order first to ensure it exists
            await controller.CreateOrder(order);

            // Now modify the order (e.g., update the price)
            order.Price = 150.0m;  // Update Price instead of TotalAmount

            // Call the UpdateOrder method
            var result = await controller.UpdateOrder(order.Id, order);

            // Verify that the result is a NoContentResult (indicating the update was successful)
            var actionResult = Assert.IsType<NoContentResult>(result);

            // Optionally, check if the updated value is correct by fetching the order again
            var updatedOrder = await context.Orders.FindAsync(order.Id);
            Assert.Equal(150.0m, updatedOrder.Price);  // Check that the price has been updated
        }
    }
}
