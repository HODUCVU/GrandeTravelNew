using GrandeTravel.Models;

namespace GrandeTravel.ViewModels
{
    public class DisplayAllTravelPackagesViewModel
    {
        public int Total { get; set; }
        public IEnumerable<TravelPackage> TravelPackageList { get; set; }

        public string? searchList;
    }
}
