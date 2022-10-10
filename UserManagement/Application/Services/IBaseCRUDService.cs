using Application.Helpers;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IBaseCRUDService<TModel, TEntity, TSearch, TInsert, TUpdate>
        where TModel : class where TEntity : class where TSearch : class where TInsert : class where TUpdate : class
    {
        public Task<PagedList<TEntity, TModel>> GetAsync(TSearch search);

        public Task<TModel> GetByIdAsync(int id);

        public Task<TModel> InsertAsync(TInsert request);

        public Task<TModel> UpdateAsync(int id, TUpdate model);

        public Task<TModel> DeleteAsync(int id);
    }
}