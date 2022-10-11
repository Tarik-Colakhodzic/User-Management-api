using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Requests.UserPermissions;
using AutoMapper;

namespace Application.Services.UserPermissions
{
    public class UserPermissionsService : BaseCRUDService<UserPermissionsModel, Domain.Entities.UserPermissions, object, UserPermissionsInsertRequest, object>, IUserPermissionsService
    {
        public UserPermissionsService(IApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}