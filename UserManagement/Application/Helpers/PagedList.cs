using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class PagedList<TEntity, TModel> : List<TModel>
    {
        protected readonly IMapper _mapper;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedList(List<TEntity> items, int pageNumber, int pageSize, IMapper mapper)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            _mapper = mapper;

            AddRange(_mapper.Map<IList<TModel>>(items));
        }

        public static async Task<PagedList<TEntity, TModel>> CreateAsync(IQueryable<TEntity> source, IMapper mapper, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var items = await source.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
            return new PagedList<TEntity, TModel>(items, pageNumber, pageSize, mapper);
        }
    }
}