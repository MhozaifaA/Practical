using eDataPracticalTest;
using eDataPracticalTest.DataTransferObjects;
using eDataPracticalTest.Domain;
using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;

namespace eDataEmployeeAPI.Controllers
{
    [ApiController]
    [AuthorizeJwt]
    [Route("api/[controller]/[action]")]
    public class DepartmentController : MrControllerBaseGeneral<Guid, IDepartmentService,DepartmentDto>
    {
        public DepartmentController(IDepartmentService repository) : base(repository) { }
    }
}
