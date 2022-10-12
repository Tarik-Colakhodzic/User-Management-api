using Application.Models.Responses;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCRUDController<TModel, TEntity, TSearch, TInsert, TUpdate> : ControllerBase
        where TModel : class where TEntity : class where TSearch : class where TInsert : class where TUpdate : class
    {
        protected readonly IBaseCRUDService<TModel, TEntity, TSearch, TInsert, TUpdate> _service;

        public BaseCRUDController(IBaseCRUDService<TModel, TEntity, TSearch, TInsert, TUpdate> servicce)
        {
            _service = servicce;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync([FromQuery] TSearch search)
        {
            var entities = await _service.GetAsync(search);
            return Ok(new PagedResponse<IEnumerable<TModel>>(entities, entities.PageNumber, entities.PageSize));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public virtual async Task<IActionResult> InsertAsync([FromBody] TInsert request)
        {
            return Ok(await _service.InsertAsync(request));
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync(int id, [FromBody] TUpdate model)
        {
            return Ok(await _service.UpdateAsync(id, model));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}