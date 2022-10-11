using Application.Models;
using Application.Models.Requests.UserPermissions;

namespace Application.Services.UserPermissions
{
    public interface IUserPermissionsService : IBaseCRUDService<UserPermissionsModel, Domain.Entities.UserPermissions, object, UserPermissionsInsertRequest, object>
    {
    }
}