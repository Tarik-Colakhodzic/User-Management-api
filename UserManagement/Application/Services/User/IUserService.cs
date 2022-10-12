using Application.Models;
using Application.Models.Requests.User;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public interface IUserService : IBaseCRUDService<UserModel, Domain.Entities.User, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
        public Task<UserModel> GetByIdAsync(int id, string includeItems);
    }
}