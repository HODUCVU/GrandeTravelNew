using System.ComponentModel.DataAnnotations;

namespace GrandeTravel.ViewModels
{
    public class CreateBookingViewModel
    {
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        public int TravelPackageId { get; set; }

        [Display(Name = "Number of People")]
        [Range(1, 20, ErrorMessage = "Please enter Atleast 1 Person and No more than 20 People")]
        public int People { get; set; }

        public int TotalCost { get; set; }

        public string TravelPackageName { get; set; }
    }
}
