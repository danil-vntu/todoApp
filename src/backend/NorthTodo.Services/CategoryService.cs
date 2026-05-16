using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NorthTodo.Interfaces.DTOs.Categories;
using NorthTodo.Interfaces.Entities;
using NorthTodo.Interfaces.Interfaces;

namespace NorthTodo.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int categoryId, int userId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) 
                throw new KeyNotFoundException("Category is not found");
            if (category.UserId != userId) 
                throw new UnauthorizedAccessException("You do not have access to this category");

            return _mapper.Map<CategoryResponseDto>(category); ;
        }
        public async Task<IEnumerable<CategoryResponseDto>> GetUsersCategoriesAsync(int userId)
        {
            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
            if (categories == null) return Enumerable.Empty<CategoryResponseDto>();

            return _mapper.Map<List<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync
            (CategoryCreateUpdateDto categoryDto, int userId)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.UserId = userId;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<CategoryResponseDto> UpdateCategoryAsync
            (CategoryCreateUpdateDto categoryDto, int categoryId, int userId)
        {
            var currentCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (currentCategory == null) 
                throw new KeyNotFoundException("Category is not found");
            if (currentCategory.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this category");
            var category = _mapper.Map(categoryDto, currentCategory);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId, int userId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) 
                throw new KeyNotFoundException("Category is not found");
            if (category.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this category");

            foreach (var task in _context.TaskItems.Where(t => t.CategoryId == categoryId))
            {
                task.CategoryId = null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
