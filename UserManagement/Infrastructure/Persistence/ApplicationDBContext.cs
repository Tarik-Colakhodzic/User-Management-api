using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }

        public Task<int> SaveChangesAsync()
        {
            AddAuditInfo();
            return base.SaveChangesAsync();
        }

        private void AddAuditInfo()
        {
            var entities = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            var utcNow = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.DateCreated = utcNow;
                }
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.DateModified = utcNow;
                }
            }
        }
    }
}