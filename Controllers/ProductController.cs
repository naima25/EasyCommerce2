using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCommerce.DTO;
using EasyCommerce.Interfaces;
using EasyCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EasyCommerce.Data;

namespace EasyCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly EasyCommerceContext _context;

        // Updated constructor to inject BOTH services + context
        public ProductController(
            IProductService productService,
            ILogger<ProductController> logger,
            EasyCommerceContext context
        )
        {
            _productService = productService;
            _logger = logger;
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                _logger.LogInformation("Fetching all products.");
                var products = await _productService.GetAllProductsAsync();

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products found.");
                    return NotFound("No products found.");
                }

                // This is where the mapping happens - replace your existing Select statement
                var productDTOs = products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Featured = p.Featured,
                    ImageUrl = p.ImageUrl,  // Your newly added property
                    CategoryIds = p.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                    CategoryNames = p.ProductCategories.Select(pc => pc.Category.Name).ToList(),
                }).ToList();  // Added ToList() for better practice

                return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching product with ID {id}");
                var product = await _productService.GetProductByIdAsync(id);

                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound($"Product with ID {id} not found.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/Product/byCategory?categoryName=Electronics
        [HttpGet("byCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory([FromQuery] string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return BadRequest("Category name is required.");
            }

            var products = await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Where(p => p.ProductCategories.Any(pc => pc.Category.Name == categoryName))
                .ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found for the given category.");
            }

            return Ok(products);
        }

        // GET: api/Product/featured
        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFeaturedProducts()
        {
            try
            {
                _logger.LogInformation("Fetching featured products");
                var featuredProducts = await _productService.GetFeaturedProductsAsync();
                return Ok(featuredProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch featured products");
                return StatusCode(500, new
                {
                    Message = "An error occurred while fetching featured products.",
                    Details = ex.Message,
                });
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogWarning("Received empty product object.");
                    return BadRequest("Product data cannot be null.");
                }

                await _productService.AddProductAsync(product);
                _logger.LogInformation($"Product with ID {product.Id} created.");
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    _logger.LogWarning("Product ID mismatch.");
                    return BadRequest("Product ID mismatch.");
                }

                await _productService.UpdateProductAsync(id, product);
                _logger.LogInformation($"Product with ID {id} updated.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _productService.GetProductByIdAsync(id) == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found for update.");
                    return NotFound($"Product with ID {id} not found.");
                }
                else
                {
                    _logger.LogError("Error updating product.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound($"Product with ID {id} not found.");
                }

                await _productService.DeleteProductAsync(id);
                _logger.LogInformation($"Product with ID {id} deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
