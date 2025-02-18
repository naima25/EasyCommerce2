using EasyCommerce.Models; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


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
    }

}
