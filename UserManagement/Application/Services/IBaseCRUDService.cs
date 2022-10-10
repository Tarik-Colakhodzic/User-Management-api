using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IBaseCRUDService<TModel, TEntity, TSearch, TInsert, TUpdate> 
        where TModel : class where TEntity : class where TSearch : class where TInsert : class where TUpdate : class
    {
        public Task<IEnumerable<TModel>> GetAsync();

        public Task<TModel> GetByIdAsync(int id);

        public Task<TModel> InsertAsync(TInsert request);

        public Task<TModel> UpdateAsync(int id, TUpdate model);

        public Task<TModel> DeleteAsync(int id);
    }
}