using Application.Common.Interfaces;
using Application.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public PermissionService(IApplicationDBContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<PermissionModel>> GetAllAsync()
        {
            var permissions = _memoryCache.Get<List<PermissionModel>>("permissions");

            if(permissions == null)
            {
                permissions = _mapper.Map<List<PermissionModel>>(await _context.Permissions.ToListAsync());
                _memoryCache.Set("permissions", permissions, TimeSpan.FromDays(1));
            }
             
            return permissions;
        }
    }
}