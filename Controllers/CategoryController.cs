using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EasyCommerce.Models; // Make sure to include the correct namespace

namespace EasyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        // Sample in-memory data
        private static List<Category> Categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Clothing" },
            new Category { Id = 3, Name = "Home & Kitchen" }
        };

        // GET: api/Category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return Ok(Categories);
        }

        // GET: api/Category/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = Categories.Find(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public ActionResult<Category> Create(Category newCategory)
        {
            newCategory.Id = Categories.Count + 1; // Assigning an ID (simple approach for in-memory data)
            Categories.Add(newCategory);
            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        public ActionResult<Category> Update(int id, Category updatedCategory)
        {
            var existingCategory = Categories.Find(c => c.Id == id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Update properties
            existingCategory.Name = updatedCategory.Name;

            return Ok(existingCategory);
        }

        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var categoryToRemove = Categories.Find(c => c.Id == id);
            if (categoryToRemove == null)
            {
                return NotFound();
            }

            Categories.Remove(categoryToRemove);
            return NoContent();
        }
    }
}
