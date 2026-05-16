using NorthTodo.Interfaces.DTOs.Categories;
using NorthTodo.Interfaces.Entities;

namespace NorthTodo.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto?> GetCategoryByIdAsync(int categoryId, int userId);
        Task<IEnumerable<CategoryResponseDto>> GetUsersCategoriesAsync(int userId);
        Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateUpdateDto categoryDto, int userId);
        Task<CategoryResponseDto> UpdateCategoryAsync
            (CategoryCreateUpdateDto categoryDto, int categoryId, int userId);
        Task<bool> DeleteCategoryAsync(int categoryId, int userId);
    }
}
