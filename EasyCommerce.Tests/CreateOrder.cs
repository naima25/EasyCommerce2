﻿using Microsoft.AspNetCore.Mvc;
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
            var controller = new OrderController(orderService, logger);

            // Prepare a sample order for testing
            var order = new Order { OrderDate = DateTime.Now, TotalAmount = 100.0m };  

            // Call the method we want to test (CreateOrder) with the sample order
            var result = await controller.CreateOrder(order);

            // Verify that the result is of type ActionResult
            var actionResult = Assert.IsType<ActionResult<Order>>(result); 

           
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result); 
            var returnedOrder = Assert.IsType<Order>(createdAtActionResult.Value); // Ensure the result value is of type Order

            
            Assert.Equal(order.TotalAmount, returnedOrder.TotalAmount);  // Check that the order's total amount matches
        }
    }
}
