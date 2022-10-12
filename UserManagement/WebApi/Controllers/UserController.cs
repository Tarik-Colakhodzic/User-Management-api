using Application.Models;
using Application.Models.Requests.User;
using Application.Models.Requests.UserPermissions;
using Application.Services.User;
using Application.Services.UserPermissions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseCRUDController<UserModel, User, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
        private IUserService _userService;
        private readonly IUserPermissionsService _userPermissionsService;

        public UserController(IUserService service, IUserPermissionsService userPermissionsService) : base(service)
        {
            _userService = service;
            _userPermissionsService = userPermissionsService;
        }

        [HttpGet("{id}/userPermissions")]
        public virtual async Task<IActionResult> GetUserPermissions(int id, [FromQuery] string includeItems)
        {
            return Ok(await _userService.GetByIdAsync(id, includeItems));
        }

        [HttpPost("userPermissions")]
        public virtual async Task<IActionResult> InsertUserPermissionAsync([FromBody] UserPermissionsInsertRequest insertRequest)
        {
            return Ok(await _userPermissionsService.InsertAsync(insertRequest));
        }

        [HttpDelete("userPermissions/{userPermissionId}")]
        public virtual async Task<IActionResult> DeleteUserPermissionAsync(int userPermissionId)
        {
            return Ok(await _userPermissionsService.DeleteAsync(userPermissionId));
        }
    }
}