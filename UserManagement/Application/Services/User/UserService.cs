using Application.Common.Interfaces;
using Application.Helpers;
using Application.Models;
using Application.Models.Requests.User;
using AutoMapper;
using Domain.Entities;
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

                if (search.IncludeList?.Any() ?? false)
                {
                    foreach (var includeEntity in search.IncludeList)
                    {
                        entity = entity.Include(includeEntity);
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

        public override async Task<UserModel> UpdateAsync(int id, UserUpdateRequest model)
        {
            var entity = await _entity.Include(x => x.UserPermissions).FirstOrDefaultAsync(x => x.Id == id);
            _mapper.Map(model, entity);

            var permissionIdsToRemove = entity.UserPermissions.Select(x => x.PermissionId).Except(model.Permissions);
            var permissionIdsToAdd = model.Permissions.Except(entity.UserPermissions.Select(x => x.Id));

            foreach (var permissionId in permissionIdsToAdd)
            {
                var userPermission = new UserPermissions
                {
                    UserId = entity.Id,
                    PermissionId = permissionId
                };

                await _context.UserPermissions.AddAsync(userPermission);
            }
            _context.UserPermissions.RemoveRange(entity.UserPermissions.Where(x => permissionIdsToRemove.Any(y => y == x.PermissionId)));

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
    }
}