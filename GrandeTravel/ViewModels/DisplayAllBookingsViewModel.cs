using GrandeTravel.Models;

namespace GrandeTravel.ViewModels
{
    public class DisplayAllBookingsViewModel
    {
        public int total { get; set; }
        public IEnumerable<Booking>? Bookings { get; set; }
    }
}
