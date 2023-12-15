using System.ComponentModel.DataAnnotations;

namespace GrandeTravel.ViewModels
{
    public class UpdateCustomerProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
    }
}
