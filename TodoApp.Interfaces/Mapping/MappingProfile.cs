using TodoApp.Interfaces.Entities;
using TodoApp.Interfaces.DTOs.Categories;
using TodoApp.Interfaces.DTOs.Auth;

using AutoMapper;
using TodoApp.Interfaces.DTOs.Users;

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
        } 
    }
}
