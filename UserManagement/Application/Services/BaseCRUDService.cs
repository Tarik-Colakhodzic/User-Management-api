using Application.Common.Interfaces;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BaseCRUDService<TModel, TEntity, TSearch, TInsert, TUpdate>
        where TModel : class where TEntity : class where TSearch : class where TInsert : class where TUpdate : class
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _entity;

        public BaseCRUDService(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _entity = _context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TModel>> GetAsync()
        {
            return _mapper.Map<List<TModel>>(await _entity.ToListAsync());
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            return _mapper.Map<TModel>(await _entity.FindAsync(id));
        }

        public virtual async Task<TModel> InsertAsync(TInsert model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _entity.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> UpdateAsync(int id, TUpdate model)
        {
            var entity = await _entity.FindAsync(id);
            _mapper.Map(model, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> DeleteAsync(int id)
        {
            var entity = await _entity.FindAsync(id);
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                _entity.Update(entity);
            }
            else
            {
                _entity.Remove(entity);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<TModel>(entity);
        }
    }
}