using Application.Models;
using Application.Services.Permission;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService service)
        {
            _permissionService = service;
        }

        [HttpGet("all")]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _permissionService.GetAllAsync());
        }
    }
}