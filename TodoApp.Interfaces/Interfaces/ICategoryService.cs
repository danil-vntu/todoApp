using TodoApp.Interfaces.DTOs.Categories;
using TodoApp.Interfaces.Entities;

namespace TodoApp.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<Category?> GetCategoryByIdAsync(int categoryId, int userId);
        Task<IEnumerable<Category>> GetUsersCategoriesAsync(int userId);
        Task<Category> CreateCategoryAsync(CategoryCreateUpdateDto categoryDto, int userId);
        Task<Category> UpdateCategoryAsync
            (CategoryCreateUpdateDto categoryDto, int categoryId, int userId);
        Task<bool> DeleteCategoryAsync(int categoryId, int userId);
    }
}
