using TodoApp.Interfaces.DTOs.Categories;
using TodoApp.Interfaces.Entities;

namespace TodoApp.Interfaces.Interfaces
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
