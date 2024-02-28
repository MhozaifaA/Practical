using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public class MrControllerBaseGeneral<TKey, TRepository, TDto> : MrControllerBase<TKey, TRepository> where TKey : struct, IEquatable<TKey>
        where TRepository : IMrRepositoryGeneral<TKey, TDto> where TDto : ISelector<TDto>
    {

        public MrControllerBaseGeneral(TRepository repository) : base(repository) { }

        [HttpGet]
        public virtual async Task<IActionResult> Fetch() => await repository.FetchAsync().ToJsonResultAsync();

        [HttpGet]
        public virtual async Task<IActionResult> GetById(TKey id) => await repository.GetByIdAsync(id).ToJsonResultAsync();

        [HttpPost]
        public virtual async Task<IActionResult> Add(TDto dto) => await repository.AddAsync(dto).ToJsonResultAsync();

        [HttpPut]
        public virtual async Task<IActionResult> Update(TDto dto) => await repository.UpdateAsync(dto).ToJsonResultAsync();

        [HttpPut]
        public virtual async Task<IActionResult> Modify(TDto dto) => await repository.ModifyAsync(dto).ToJsonResultAsync();

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] TKey id) => await repository.DeleteAsync(id).ToJsonResultAsync();

        //[HttpPost]
        //public virtual async Task<IActionResult> AddMulti(IEnumerable<TDto> dtos) => await repository.AddMultiAsync(dtos).ToJsonResultAsync();

        //[HttpPut]
        //public virtual async Task<IActionResult> UpdateMulti(IEnumerable<TDto> dtos) => await repository.UpdateMultiAsync(dtos).ToJsonResultAsync();

        //[HttpPut]
        //public virtual async Task<IActionResult> ModifyMulti(IEnumerable<TDto> dtos) => await repository.ModifyMultiAsync(dtos).ToJsonResultAsync();

        //[HttpDelete]
        //public virtual async Task<IActionResult> DeleteMulti(IEnumerable<TKey> ids) => await repository.DeleteMultiAsync(ids).ToJsonResultAsync();

    }
}
