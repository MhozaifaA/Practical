using eDataPracticalTest.DataTransferObjects;
using eDataPracticalTest.DataTransferObjects.Security;
using eDataPracticalTest.Domain;
using Meteors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDataEmployeeAPI.Controllers.Security
{
    [ApiController]
    [Route("api/security/[controller]/[action]")]
    public class AccountController : MrControllerBase<Guid, IAccountRepository>
    {
        public AccountController(IAccountRepository repository) : base(repository) { }

        /// <summary>
        /// { username:admin, password:admin}
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]        
        public async Task<ActionResult<AccessTokenDto>> Login([FromBody] LoginDto dto)
        {
            return await repository.Login(dto).ToJsonResultAsync();
        }

    }
}
