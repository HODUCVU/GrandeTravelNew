using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeTravel.Models
{
    [Table("tb_MyUser")]
    public class MyUser : IdentityUser
    {
        IEnumerable<TravelPackage>? TravelPackages { get; set; }
        IEnumerable<Booking>? Bookings { get; set; }
        IEnumerable<Feedback>? Feedbacks { get; set; }
    }
}
