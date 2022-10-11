using Application.Models;
using Application.Models.Requests.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserPermissions, UserPermissionsModel>().ReverseMap();
            CreateMap<Permission, PermissionModel>().ReverseMap();
            CreateMap<User, UserInsertRequest>().ReverseMap();
            CreateMap<User, UserUpdateRequest>().ReverseMap();
        }
    }
}