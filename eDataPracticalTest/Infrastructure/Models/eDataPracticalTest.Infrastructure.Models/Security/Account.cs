using Meteors;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Securty;
using System.ComponentModel.DataAnnotations;


namespace eDataPracticalTest.Infrastructure.Models
{
    /* Add extrac security for our api, like Admin controller should add employee not public
        simple use account, can be use the default of IdentityUser<TIndex>,
     */
    public class Account : MrIdentityUser<Guid>, INominal
    {

        [ColumnDataType(DataBaseTypes.NVARCHAR, TypeConstants.MediumString)]
        [Required]
        public string Name { get; set; }
    }
}
