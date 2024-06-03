using EFCoreExample.DTOs;
using EFCoreExample.Models;

namespace EFCoreExample.Services
{

        /*We use the interface because the Dependency COntainer prefers having the most generic version of the dependency to work with.
        It will look
        */
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO>GetAllCategories();

        CategoryDTO GetCategoryById(int CategoryId);

        Category CreateCategory(CategoryDTO CategoryDto);

        void UpdateCategory(int CategoryId, CategoryDTO UpdatedCategory);

        void DeleteCategory(int CategoryId);

    }
}