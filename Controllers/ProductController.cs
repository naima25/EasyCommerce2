using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EasyCommerce.Models;

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // Sample in-memory data
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m, CategoryId = 1 },
            new Product { Id = 2, Name = "Product 2", Price = 20.99m, CategoryId = 1 },
            new Product { Id = 3, Name = "Product 3", Price = 15.49m, CategoryId = 2 }
        };

        // GET: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(Products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = Products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult<Product> Create(Product newProduct)
        {
            newProduct.Id = Products.Count + 1;  // Assigning an ID (simple approach for in-memory data)
            Products.Add(newProduct);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public ActionResult<Product> Update(int id, Product updatedProduct)
        {
            var existingProduct = Products.Find(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update properties
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.CategoryId = updatedProduct.CategoryId;

            return Ok(existingProduct);
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var productToRemove = Products.Find(p => p.Id == id);
            if (productToRemove == null)
            {
                return NotFound();
            }

            Products.Remove(productToRemove);
            return NoContent();
        }
    }
}
