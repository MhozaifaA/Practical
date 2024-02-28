using eDataPracticalTest.DataTransferObjects;
using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.Domain
{
    public interface IEmployeeService : IMrRepositoryGeneral<Guid, Employee, EmployeeDto>
    {
        Task<OperationResult<IEnumerable<EmployeeDto>>> List();
    }
}
