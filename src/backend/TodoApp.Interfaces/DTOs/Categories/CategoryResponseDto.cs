using TodoApp.Interfaces.DTOs.Tasks;

namespace TodoApp.Interfaces.DTOs.Categories
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
