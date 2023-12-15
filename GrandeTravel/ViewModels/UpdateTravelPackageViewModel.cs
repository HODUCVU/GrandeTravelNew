using System.ComponentModel.DataAnnotations;

namespace GrandeTravel.ViewModels
{
    public class UpdateTravelPackageViewModel
    {
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }
        public string Location { get; set; }
        [Display(Name = "Photo")]
        public string PhotoLocation { get; set; }
        [Display(Name = "Package Description")]
        public string PackageDescription { get; set; }
        [Display(Name = "Package Price")]
        [DataType(DataType.Currency)]
        public int PackagePrice { get; set; }
    }
}
