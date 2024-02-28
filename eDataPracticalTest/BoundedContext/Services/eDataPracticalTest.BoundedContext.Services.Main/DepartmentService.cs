using eDataPracticalTest.DataTransferObjects;
using eDataPracticalTest.Domain;
using eDataPracticalTest.Infrastructure.Databases.SqlServer;
using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.BoundedContext
{
    [AutoService]
    public class DepartmentService : MrRepositoryGeneral<AppDbContext, Guid, Department, DepartmentDto>, IDepartmentService
    {
        public DepartmentService(AppDbContext context) : base(context)
        {
        }

    }
}
