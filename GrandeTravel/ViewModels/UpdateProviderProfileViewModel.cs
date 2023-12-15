using System.ComponentModel.DataAnnotations;

namespace GrandeTravel.ViewModels
{
    public class UpdateProviderProfileViewModel
    {
        [Required, Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
    }
}
