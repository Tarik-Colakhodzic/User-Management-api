using Application.Models;

namespace Application.Services.Permission
{
    public interface IPermissionService : IBaseCRUDService<PermissionModel, Domain.Entities.Permission, object, object, object>
    {
    }
}