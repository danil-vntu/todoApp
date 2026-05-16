using NorthTodo.Interfaces.Entities;
using NorthTodo.Interfaces.DTOs.Categories;
using NorthTodo.Interfaces.DTOs.Auth;
using NorthTodo.Interfaces.DTOs.Users;
using NorthTodo.Interfaces.DTOs.Tasks;
using NorthTodo.Interfaces.Models;

using AutoMapper;

namespace NorthTodo.Interfaces.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(m => m.PasswordHash, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<JwtTokenResult, AuthResponseDto>();

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
