using Application.Models;
using Application.Services.Permission;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : BaseCRUDController<PermissionModel, Permission, object, object, object>
    {
        public PermissionController(IPermissionService service) : base(service)
        {
        }
    }
}