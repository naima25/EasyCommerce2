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
    public class OrderControllerTests
    {
        [Fact]
        public async Task CreateOrder_ReturnsCreatedResult()
        {
            var options = new DbContextOptionsBuilder<EasyCommerceContext>()
                .UseInMemoryDatabase("TestDB")  // Using an in-memory database for testing
                .Options;

            using var context = new EasyCommerceContext(options);
            var orderService = new OrderService(context);  // Service for interacting with the DB

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<OrderController>();  // Real logger for capturing logs

            // Create the controller with the real logger and order service
            var controller = new OrderController(context, logger);
    

            // Prepare a sample order for testing (without TotalAmount, now using Price)
            var order = new Order 
            { 
                OrderDate = DateTime.Now, 
                Price = 100.0m  // Use Price instead of TotalAmount
            };

            // Call the method we want to test (CreateOrder) with the sample order
            var result = await controller.CreateOrder(order);

            // Verify that the result is of type ActionResult
            var actionResult = Assert.IsType<ActionResult<Order>>(result); 

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result); 
            var returnedOrder = Assert.IsType<Order>(createdAtActionResult.Value); // Ensure the result value is of type Order

            // Assert that the Price of the created order is correct
            Assert.Equal(order.Price, returnedOrder.Price);  // Check that the order's Price matches
            Assert.Equal(order.OrderDate, returnedOrder.OrderDate);  // Check that the order's OrderDate matches
        }
    }
}
