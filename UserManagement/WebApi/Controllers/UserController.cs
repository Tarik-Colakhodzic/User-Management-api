using Application.Models;
using Application.Models.Requests.User;
using Application.Services.User;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseCRUDController<UserModel, User, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
        public UserController(IUserService service) : base(service)
        {
        }
    }
}