using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.DataTransferObjects
{
    public class DepartmentDto : IIndex<Guid>, INominal, ISelector<Department, DepartmentDto>
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TypeConstants.LongNounString)]
        public string Name { get; set; }



        public static Expression<Func<Department, DepartmentDto>> Selector { get; set; } = o => new DepartmentDto()
        {
            Id = o.Id,
            Name = o.Name,
        };
        public static Expression<Func<DepartmentDto, Department>> InverseSelector { get; set; } = o => new Department()
        {
            Id = o.Id,
            Name = o.Name,
        };
        public static Action<DepartmentDto, Department> AssignSelector { get; set; } = (o, entity) =>
        {
            entity.Name = o.Name;
        };
    }
}
