using EasyCommerce.Data; // Add the namespace for EasyCommerceContext
using EasyCommerce.Models; // Your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For async database methods

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly EasyCommerceContext _context; // DbContext for SQLite

        public CategoryController(EasyCommerceContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            // Get all categories from the database
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // GET: api/Category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            // Find a category by ID from the database
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category newCategory)
        {
            // Add a new category to the database
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return the created category
            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Update(int id, Category updatedCategory)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Update the category properties
            existingCategory.Name = updatedCategory.Name;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingCategory);
        }

        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryToRemove = await _context.Categories.FindAsync(id);
            if (categoryToRemove == null)
            {
                return NotFound();
            }

            // Remove the category from the database
            _context.Categories.Remove(categoryToRemove);
            await _context.SaveChangesAsync(); // Save the changes to the database

            return NoContent(); // Return success
        }
    }
}
