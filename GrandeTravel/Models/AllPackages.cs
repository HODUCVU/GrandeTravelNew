using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeTravel.Models
{
    [Table("tb_AllPackages")]
    public class AllPackages
    {
        public IEnumerable<TravelPackage>? packages { get; set; }
    }
}
