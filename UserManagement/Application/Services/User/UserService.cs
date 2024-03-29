﻿using Application.Common.Interfaces;
using Application.Helpers;
using Application.Models;
using Application.Models.Requests.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public class UserService : BaseCRUDService<UserModel, Domain.Entities.User, UserSearchRequest, UserInsertRequest, UserUpdateRequest>, IUserService
    {
        public UserService(IApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<PagedList<Domain.Entities.User, UserModel>> GetAsync(UserSearchRequest search)
        {
            var entity = _context.Users.AsQueryable();

            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.FirstName))
                {
                    entity = entity.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.LastName))
                {
                    entity = entity.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.Email))
                {
                    entity = entity.Where(x => x.Email.ToLower().Contains(search.Email.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.Username))
                {
                    entity = entity.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
                }
                if (search.Status.HasValue)
                {
                    entity = entity.Where(x => x.Status == search.Status);
                }
                if (search.IsDeleted.HasValue)
                {
                    entity = entity.Where(x => x.IsDeleted == search.IsDeleted);
                }

                if (search.IncludeList?.Any() ?? false)
                {
                    foreach (var includeEntity in search.IncludeList)
                    {
                        entity = entity.Include(includeEntity);
                    }
                }

                if (!string.IsNullOrEmpty(search.SortColumn) && !string.IsNullOrEmpty(search.SortDirection))
                {
                    var isAsc = search.SortDirection.ToLower() == "asc";
                    var user = new Domain.Entities.User();
                    var propertyName = user.GetType().GetProperty(search.SortColumn).Name;
                    if (nameof(user.FirstName) == propertyName)
                    {
                        entity = isAsc ? entity.OrderBy(x => x.FirstName.ToLower()) : entity.OrderByDescending(x => x.FirstName.ToLower());
                    }
                    else if (nameof(user.LastName) == propertyName)
                    {
                        entity = isAsc ? entity.OrderBy(x => x.LastName.ToLower()) : entity.OrderByDescending(x => x.LastName.ToLower());
                    }
                    else if (nameof(user.Email) == propertyName)
                    {
                        entity = isAsc ? entity.OrderBy(x => x.Email.ToLower()) : entity.OrderByDescending(x => x.Email.ToLower());
                    }
                    else if (nameof(user.Username) == propertyName)
                    {
                        entity = isAsc ? entity.OrderBy(x => x.Username.ToLower()) : entity.OrderByDescending(x => x.Username.ToLower());
                    }
                    else if (nameof(user.Username) == propertyName)
                    {
                        entity = isAsc ? entity.OrderBy(x => x.Status) : entity.OrderByDescending(x => x.Status);
                    }
                }
            }

            return await PagedList<Domain.Entities.User, UserModel>.CreateAsync(entity, _mapper, search.PageNumber, search.PageSize);
        }

        public override async Task<UserModel> InsertAsync(UserInsertRequest model)
        {
            var entity = _mapper.Map<Domain.Entities.User>(model);

            entity.PasswordSalt = GenerateSalt();
            entity.PasswordHash = GenerateHash(entity.PasswordSalt, model.Password);

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(entity);
        }

        private string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        private string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        public async Task<UserModel> GetByIdAsync(int id, string includeItems)
        {
            var entity = _entity.AsQueryable();
            if (!string.IsNullOrEmpty(includeItems))
            {
                var includeArray = includeItems.Split(",");
                foreach (var include in includeArray)
                {
                    entity = entity.Include(include);
                }
            }
            return _mapper.Map<UserModel>(await entity.FirstOrDefaultAsync(x => x.Id == id));
        }
    }
}