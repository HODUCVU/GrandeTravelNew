using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeTravel.Models
{
    [Table("tb_TravelProviderProfile")]
    public class TravelProviderProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TravelProviderProfileId { get; set; }
        public string? UserId { get; set; }
        public string? CompanyName { get; set; }
        public int Phone { get; set; }
    }
}
