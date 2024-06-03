using Microsoft.AspNetCore.Mvc;
using EFCoreExample.Data;
using EFCoreExample.DTOs;
using EFCoreExample.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        //This appdbcontext is used to interact with our database
        //The constructor is used by our dependency manager to inject it into our class


        private readonly AppDbContext _context;


        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }




         [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = _context.Categories
                .Include(c => c.Products)
                .Select(c => new CategoryDTO
                {
                    Name = c.Name,
                }).ToList();

            return categories;
        }



        //added
         [HttpGet("{CategoriesId}")]
        public ActionResult<ProductDTO> GetCategoryById(int CategoryId)
        {
            var category = _context.Categories.Find(CategoryId);
            var categoryDto = new ProductDTO
            {
                Name = category.Name,
            };
            return categoryDto;
        }





         [HttpPost]
        public ActionResult<CategoryDTO> PostCategory(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Products = []
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategories), new { CategoryId = category.CategoryId }, categoryDto);
        }




       [HttpPut("{CategoryId}")]
        public ActionResult<CategoryDTO> UpdateCategory(int CategoryId, CategoryDTO UpdatedCategory)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);

            category.Name = UpdatedCategory.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return Ok(UpdatedCategory);
        }

         //added
            [HttpDelete("{CategoryId}")]
        public IActionResult DeleteCategory(int CategoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }


    }

}