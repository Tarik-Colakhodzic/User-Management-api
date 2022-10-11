using Application.Common.Interfaces;
using Application.Models;
using AutoMapper;

namespace Application.Services.Permission
{
    public class PermissionService : BaseCRUDService<PermissionModel, Domain.Entities.Permission, object, object, object>, IPermissionService
    {
        public PermissionService(IApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}