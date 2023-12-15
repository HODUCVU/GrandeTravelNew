using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeTravel.Models
{
    [Table("tb_Booking")]
    public class Booking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int TravelPackageId { get; set; }
        public TravelPackage? TravelPackage { get; set; }
        public string? MyUserId { get; set; }
        public MyUser? MyUser { get; set; }
        public string? Name { get; set; }
        public int People { get; set; }
        public int TotalCost { get; set; }
        public string? TravelPackageName { get; set; }
        public string? VoucherCode { get; set; }
        public bool LeftFeedback { get; set; }
        public bool PaymentReceived { get; set; }
    }
}
