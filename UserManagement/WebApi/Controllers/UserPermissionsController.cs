using Application.Models;
using Application.Models.Requests.UserPermissions;
using Application.Services.UserPermissions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPermissionsController : BaseCRUDController<UserPermissionsModel, UserPermissions, object, UserPermissionsInsertRequest, object>
    {
        public UserPermissionsController(IUserPermissionsService service) : base(service)
        {
        }
    }
}