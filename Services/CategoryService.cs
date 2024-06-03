using EFCoreExample.Data;
using EFCoreExample.DTOs;
using EFCoreExample.Models;

namespace EFCoreExample.Services
{
    public class CategoryService : ICategoryService
    {
         private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }


          public Category CreateCategory(CategoryDTO CategoryDto)
        {
            if (ValidateNewCategory(CategoryDto))
            {
                var category = new Category
                {
                    Name = CategoryDto.Name,
                    
                };
                _context.Categories.Add(category);
                _context.SaveChanges();

                return category;
            }  else
            {
                throw new Exception("Invalid Category");
            }
        }


          private bool ValidateNewProfile(CategoryDTO CategoryDto)
        {
            if(CategoryDto.Name.Trim().Length ==0){
                return false;
            }else return true;
        }


         public void DeleteCategory(int CategoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
        }

       
         public IEnumerable<ProfileDTO> GetAllCategories()
        {
           var categories = _context.Categories
                 .Select(c => new CategoryDTO
                {
                    Name = c.Name,
                    
                }).ToList();
                return categories;
        }



          public CategoryDTO GetCategoryById(int CategoryId)
        {
           var category = _context.Categories.Find(CategoryId);
           var categoryDto = new CategoryDTO
            {
                Name = category.Name,
                
            };
            return categoryDto;
        }



           public void UpdateCategory(int CategoryId, CategoryDTO UpdatedCategory)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);

            category.Name = UpdatedCategory.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();
        }

       
    }
}