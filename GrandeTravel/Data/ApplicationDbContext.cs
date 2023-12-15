using GrandeTravel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GrandeTravel.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TravelPackage> TblTravelPackage { get; set; }
        public DbSet<Booking> TblBooking { get; set; }
        public DbSet<Feedback> TblFeedback { get; set; }
        public DbSet<TravelProviderProfile> TblTravelProviderProfile { get; set; }
        public DbSet<CustomerProfile> TblCustomerProfile { get; set; }
        public DbSet<Photo> TblPhoto { get; set; }
    }
}
