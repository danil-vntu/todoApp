using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TodoApp.Interfaces.DTOs.Categories;
using TodoApp.Interfaces.Entities;
using TodoApp.Interfaces.Interfaces;

namespace TodoApp.Services
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

        public async Task<Category?> GetCategoryByIdAsync(int categoryId, int userId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) 
                throw new InvalidOperationException("Category is not found");
            if (category.UserId != userId) 
                throw new UnauthorizedAccessException("You do not have access to this category");
            return category;
        }
        public async Task<IEnumerable<Category>> GetUsersCategoriesAsync(int userId)
        {
            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
            if (categories == null) return Enumerable.Empty<Category>();
            return categories;
        }
        public async Task<Category> CreateCategoryAsync
            (CategoryCreateUpdateDto categoryDto, int userId)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.UserId = userId;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateCategoryAsync
            (CategoryCreateUpdateDto categoryDto, int categoryId, int userId)
        {
            var currentCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (currentCategory == null) 
                throw new InvalidOperationException("Category is not found");
            if (currentCategory.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this category");
            var category = _mapper.Map(categoryDto, currentCategory);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId, int userId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) 
                throw new InvalidOperationException("Category is not found");
            if (category.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this category");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
