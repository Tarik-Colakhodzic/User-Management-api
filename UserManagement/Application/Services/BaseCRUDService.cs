using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BaseCRUDService<TModel, TEntity, TSearch, TInsert, TUpdate>
        where TModel : class where TEntity : class where TSearch : class where TInsert : class where TUpdate : class
    {
        protected readonly IApplicationDBContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _entity;

        public BaseCRUDService(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _entity = _context.Set<TEntity>();
        }

        public virtual async Task<PagedList<TEntity, TModel>> GetAsync(TSearch search)
        {
            if (search is BaseSearchModel baseSearchModel)
            {
                var entity = _entity.AsQueryable();
                if (baseSearchModel.IncludeList?.Any() ?? false)
                {
                    foreach (var item in baseSearchModel.IncludeList)
                    {
                        entity = entity.Include(item);
                    }
                }
                return await PagedList<TEntity, TModel>.CreateAsync(entity, _mapper, baseSearchModel.PageNumber, baseSearchModel.PageSize);
            }

            return await PagedList<TEntity, TModel>.CreateAsync(_entity, _mapper);
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