using Application.Models;
using Application.Models.Requests.User;

namespace Application.Services.User
{
    public interface IUserService : IBaseCRUDService<UserModel, Domain.Entities.User, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
    }
}