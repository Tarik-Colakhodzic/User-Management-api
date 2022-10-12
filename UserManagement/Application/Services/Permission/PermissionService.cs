using Application.Common.Interfaces;
using Application.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;

        public PermissionService(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<PermissionModel>>(await _context.Permissions.ToListAsync());
        }
    }
}