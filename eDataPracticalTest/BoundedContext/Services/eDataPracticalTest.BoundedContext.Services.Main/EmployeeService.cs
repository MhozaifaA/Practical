using eDataPracticalTest.DataTransferObjects;
using eDataPracticalTest.Domain;
using eDataPracticalTest.Infrastructure.Databases.SqlServer;
using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.BoundedContext
{
    [AutoService]
    public class EmployeeService : MrRepositoryGeneral<AppDbContext, Guid, Employee, EmployeeDto>, IEmployeeService
    {
        public EmployeeService(AppDbContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<EmployeeDto>>> List()
        {
            var result = await Query.Select(x => new EmployeeDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Status = x.Status, 
                DepartmentName = x.Department.Name,
                DepartmentId = x.Department.Id,
            }).ToListAsync();
            return (result, null);
        }
    }
}
