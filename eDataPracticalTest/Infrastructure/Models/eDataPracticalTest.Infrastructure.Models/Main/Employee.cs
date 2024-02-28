using Meteors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace eDataPracticalTest.Infrastructure.Models
{
    //[Table(nameof(Employee) + "s", Schema = Schemas.dbo)]
    public class Employee : BaseEntity<Guid>
    {
        [Required]
        [ColumnDataType(DataBaseTypes.NVARCHAR, TypeConstants.LongNounString)]
        public string FirstName { get; set; }

        [Required]
        [ColumnDataType(DataBaseTypes.NVARCHAR, TypeConstants.NounString)]
        public string LastName { get; set; }

        //DateTimeOffset 
        [Required]
        [ColumnDataType(DataBaseTypes.DATETIME2)]
        public DateTime  DateOfBirth { get; set; }


        [DefaultValue(EmployeeStatus.Active)]
        public EmployeeStatus Status { get; set; }


        /// <summary>
        /// FK
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
