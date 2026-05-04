using TodoApp.Interfaces.Entities;
using TodoApp.Interfaces.DTOs.Categories;
using TodoApp.Interfaces.DTOs.Auth;
using TodoApp.Interfaces.DTOs.Users;
using TodoApp.Interfaces.DTOs.Tasks;

using AutoMapper;

namespace TodoApp.Interfaces.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(m => m.PasswordHash, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<CategoryCreateUpdateDto, Category>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.UserId, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore())
                .ForMember(m => m.Tasks, opt => opt.Ignore());


            CreateMap<User, UserProfileDto>();

            CreateMap<UserUpdateDto, User>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Email, opt => opt.Ignore())
                .ForMember(m => m.PasswordHash, opt => opt.Ignore())
                .ForMember(m => m.CreatedAt, opt => opt.Ignore())
                .ForMember(m => m.Tasks, opt => opt.Ignore())
                .ForMember(m => m.Categories, opt => opt.Ignore());

            CreateMap<TaskCreateUpdateDto, TaskItem>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreatedAt, opt => opt.Ignore())
                .ForMember(m => m.UserId, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore())
                .ForMember(m => m.Category, opt => opt.Ignore())
                .ForMember(m => m.CategoryId, opt => opt.AllowNull());

            CreateMap<TaskItem, TaskResponseDto>();

            CreateMap<Category, CategoryResponseDto>();
        } 
    }
}
