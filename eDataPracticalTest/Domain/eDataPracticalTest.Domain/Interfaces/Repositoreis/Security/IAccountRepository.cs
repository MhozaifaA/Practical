using eDataPracticalTest.DataTransferObjects.Security;
using Meteors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.Domain
{
    public interface IAccountRepository : IMrRepository
    {
        Task<OperationResult<AccessTokenDto>> Login(LoginDto dto);
    }
}
