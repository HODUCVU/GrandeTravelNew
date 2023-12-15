using System.ComponentModel.DataAnnotations;

namespace GrandeTravel.ViewModels
{
    public class CreateFeedbackViewModel
    {
        [Required]
        public string Comment { get; set; }
        [Display(Name = "")]
        public byte Rating { get; set; }

        public int TravelPackageId { get; set; }

        public int BookingId { get; set; }
    }
}
