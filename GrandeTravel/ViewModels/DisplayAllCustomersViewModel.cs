using GrandeTravel.Models;

namespace GrandeTravel.ViewModels
{
    public class DisplayAllCustomersViewModel
    {
        public IEnumerable<MyUser>? Customers { get; set; }
    }
}
