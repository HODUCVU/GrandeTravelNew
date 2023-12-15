using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeTravel.Models
{
    [Table("tb_Feedback")]
    public class Feedback
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }
        public string? Comment { get; set; }
        public byte Rating { get; set; }
        public int TravelPackageId { get; set; }
        public TravelPackage? TravelPackage { get; set; }
        public string? MyUserId { get; set; }
        public MyUser? MyUser { get; set; }
        public string? UserName { get; set; }
    }
}
