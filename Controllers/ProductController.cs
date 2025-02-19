using EasyCommerce.Data;  // Add the namespace for EasyCommerceContext
using EasyCommerce.Models; // Your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For async database methods

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly EasyCommerceContext _context; // DbContext for SQLite

        public ProductController(EasyCommerceContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            // Get all products from the database
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            // Find a product by ID from the database
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product newProduct)
        {
            // Add a new product to the database
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return the created product
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, Product updatedProduct)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the product properties
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.CategoryId = updatedProduct.CategoryId;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToRemove = await _context.Products.FindAsync(id);
            if (productToRemove == null)
            {
                return NotFound();
            }

            // Remove the product from the database
            _context.Products.Remove(productToRemove);
            await _context.SaveChangesAsync(); // Save the changes to the database

            return NoContent(); // Return success
        }
    }
}
