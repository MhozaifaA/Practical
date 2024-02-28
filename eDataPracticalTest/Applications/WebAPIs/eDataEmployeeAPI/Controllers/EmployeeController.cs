using eDataPracticalTest;
using eDataPracticalTest.DataTransferObjects;
using eDataPracticalTest.Domain;
using Meteors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;

namespace eDataEmployeeAPI.Controllers
{
    [ApiController]
    [AuthorizeJwt]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : MrControllerBase<Guid, IEmployeeService>
    {
        public EmployeeController(IEmployeeService repository) : base(repository) { }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            return await repository.List().ToJsonResultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody][Required] EmployeeDto dto)
        {
            return await repository.AddAsync(dto).ToJsonResultAsync();
        }

        [HttpPut]
        public async Task<IActionResult> Modify([FromBody][Required] EmployeeDto dto)
        {
            return await repository.ModifyAsync(dto).ToJsonResultAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute][Required] Guid id)
        {
            return await repository.DeleteAsync(id).ToJsonResultAsync();
        }

    }
}
