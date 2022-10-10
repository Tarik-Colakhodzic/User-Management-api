using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Requests.User;
using AutoMapper;

namespace Application.Services.User
{
    public class UserService : BaseCRUDService<UserModel, Domain.Entities.User, UserSearchRequest, UserInsertRequest, UserUpdateRequest>, IUserService
    {
        public UserService(IApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}