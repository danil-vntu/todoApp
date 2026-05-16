using NorthTodo.Interfaces.DTOs.Tasks;

namespace NorthTodo.Interfaces.DTOs.Categories
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
