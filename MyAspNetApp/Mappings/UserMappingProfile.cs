using AutoMapper;
using MyAspNetApp.Entities;
using MyAspNetApp.DTOs.User;

namespace MyAspNetApp.Mappings 
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserResetPasswordDto>().ReverseMap();
        }
    }
}