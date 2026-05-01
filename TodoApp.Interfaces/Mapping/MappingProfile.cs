using TodoApp.Interfaces.Entities;
using TodoApp.Interfaces.DTOs.Categories;
using TodoApp.Interfaces.DTOs.Auth;

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
        } 
    }
}
