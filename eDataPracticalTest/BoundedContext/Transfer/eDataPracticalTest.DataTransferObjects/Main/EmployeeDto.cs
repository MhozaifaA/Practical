using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace eDataPracticalTest.DataTransferObjects
{
    public class EmployeeDto : IIndex<Guid>, ISelector<Employee, EmployeeDto>
    {
        public Guid Id { get; set; }


        [Required]
        [MaxLength(TypeConstants.LongNounString)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(TypeConstants.NounString)]
        public string LastName { get; set; }

        //DateTimeOffset 
        [Required]
        public DateTime DateOfBirth { get; set; }


        public EmployeeStatus Status { get; set; }


        [Required]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "{0} is required and can't be zeors empty guid")]
        public Guid DepartmentId { get; set; }


        //projection
        public string? DepartmentName { get; set; }


        public static Expression<Func<Employee, EmployeeDto>> Selector { get; set; } = o => new EmployeeDto()
        {
            Id = o.Id,
            FirstName = o.FirstName,
            LastName = o.LastName,
            DateOfBirth = o.DateOfBirth,
            Status = o.Status,
            DepartmentId = o.DepartmentId,
        };
        public static Expression<Func<EmployeeDto, Employee>> InverseSelector { get; set; } = o => new Employee()
        {
            Id = o.Id,
            FirstName = o.FirstName,
            LastName = o.LastName,
            DateOfBirth = o.DateOfBirth,
            Status = o.Status,
            DepartmentId = o.DepartmentId,
        };
        public static Action<EmployeeDto, Employee> AssignSelector { get; set; } = (o, entity) =>
        {
            entity.FirstName = o.FirstName;
            entity.LastName = o.LastName;
            entity.DateOfBirth = o.DateOfBirth;
            entity.Status = o.Status;
            entity.DepartmentId = o.DepartmentId;
        };
    }
}
