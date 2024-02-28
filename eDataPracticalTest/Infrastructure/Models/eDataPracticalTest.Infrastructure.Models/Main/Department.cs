using Meteors;
using System.ComponentModel.DataAnnotations;

namespace eDataPracticalTest.Infrastructure.Models
{
    public class Department : BaseEntity<Guid>, INominal
    {
        public Department()
        {
            //init emps empty valid collcetion to ignore null exception when call employee navication after load data
            Employees = new HashSet<Employee>();
        }

        [Required]
        [ColumnDataType(DataBaseTypes.NVARCHAR, TypeConstants.LongNounString)]
        public required string Name { get; set; }

        //navication to get Depatemt - emps ... one-many
        public ICollection<Employee> Employees { get; set; }
    }
}
