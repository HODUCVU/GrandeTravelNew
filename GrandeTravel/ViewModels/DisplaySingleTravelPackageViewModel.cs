using GrandeTravel.Models;

namespace GrandeTravel.ViewModels
{
    public class DisplaySingleTravelPackageViewModel
    {
        public int TravelPackageId { get; set; }
        public string? PackageName { get; set; }

        public string? PhotoLocation { get; set; }

        public string? Location { get; set; }
        public string? PackageDescription { get; set; }

        public string? TravelProviderName { get; set; }

        public int PackagePrice { get; set; }

        //One TravelPackage has many Bookings
        public IEnumerable<Booking>? Bookings { get; set; }

        public string? UserName { get; set; }

        public IEnumerable<Feedback>? Feedbacks { get; set; }

        public string? longitude { get; set; }
        public string? latitude { get; set; }

        public IEnumerable<Photo>? GalleryPhotos { get; set; }
    }
}
