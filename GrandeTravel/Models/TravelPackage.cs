using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeTravel.Models
{
    [Table("tb_TravelPackage")]
    public class TravelPackage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TravelPackageId { get; set; }
        public string? PackageName { get; set; }
        public string? PhotoLocation { get; set; }
        public string? Location { get; set; }
        public string? PackageDescription { get; set; }
        public string? ProviderName { get; set; }
        public int PackagePrice { get; set; }

        public bool Discontinued { get; set; }

        public string? Longitude { get; set; }

        public string? Latitude { get; set; }

        public string? MyUserId { get; set; }
        public MyUser? MyUser { get; set; }
        //One TravelPackage has many Bookings
        public IEnumerable<Booking>? Bookings { get; set; }
        public IEnumerable<Feedback>? Feedbacks { get; set; }
        public IEnumerable<Photo>? Photos { get; set; }
    }
}
