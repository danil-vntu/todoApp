using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Extensions;
using TodoApp.Interfaces.DTOs.Categories;
using TodoApp.Interfaces.Interfaces;

namespace TodoApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersCategories()
        {
            var userId = User.GetUserId();
            var categories = await _categoryService.GetUsersCategoriesAsync(userId);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var userId = User.GetUserId();
            var category = await _categoryService.GetCategoryByIdAsync(id, userId);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateUpdateDto categoryDto)
        {
            var userId = User.GetUserId();
            var category = await _categoryService.CreateCategoryAsync(categoryDto, userId);
            return CreatedAtAction
                (nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCreateUpdateDto categoryDto)
        {
            var userId = User.GetUserId();
            var category = await _categoryService.UpdateCategoryAsync(categoryDto, id, userId);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = User.GetUserId();
            var result = await _categoryService.DeleteCategoryAsync(id, userId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
