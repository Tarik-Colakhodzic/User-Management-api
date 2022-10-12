using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Permission
{
    public interface IPermissionService
    {
        public Task<IEnumerable<PermissionModel>> GetAllAsync();
    }
}