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
    public interface IDepartmentService : IMrRepositoryGeneral<Guid, Department, DepartmentDto>
    {

    }
}
