using GrandeTravel.Data;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandeTravel.Controllers
{
    [Route("Booking")]
    public class BookingController : Controller
    {
        private IRepository<Booking> _bookingRepo;
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _travelPackageManager;
        private IEmailSender _emailService;

        public BookingController(IRepository<Booking> bookingRepo, UserManager<MyUser> userManager, IRepository<TravelPackage> travelPackageManager, IEmailSender emailService)
        {
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _travelPackageManager = travelPackageManager;
            _emailService = emailService;
        }

        // GET: /<controller>/
        [Route("Index")]
        [Authorize]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Booking> list = _bookingRepo.Query(b => b.MyUserId == userId && b.BookingDate > DateTime.Today);
            DisplayAllBookingsViewModel vm = new DisplayAllBookingsViewModel
            {
                Bookings = list,
                total = list.Count()
            };
            return View(vm);
        }
        [Route("IndexofPast")]
        [Authorize]
        public IActionResult IndexofPast()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Booking> list = _bookingRepo.Query(b => b.MyUserId == userId && b.BookingDate < DateTime.Today);
            DisplayPastBookingsViewModel vm = new DisplayPastBookingsViewModel
            {
                Bookings = list,
                total = list.Count()
            };
            return View(vm);
        }
        [Route("Create/{id}")]
        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            TravelPackage tp = _travelPackageManager.GetSingle(t => t.TravelPackageId == id);
            string today = DateTime.Now.ToString();
            CreateBookingViewModel vm = new CreateBookingViewModel
            {
                TravelPackageName = tp.PackageName,
                TotalCost = tp.PackagePrice,
                TravelPackageId = id

            };
            return View(vm);
        }
        [Route("Create/{id}")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel vm)
        {

            if (ModelState.IsValid)
            {
                TravelPackage tp = _travelPackageManager.GetSingle(t => t.TravelPackageId == vm.TravelPackageId);
                var userId = _userManager.GetUserId(User);
                string voucherCode = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                Booking booking = new Booking
                {
                    BookingDate = vm.BookingDate,
                    TravelPackageId = vm.TravelPackageId,
                    MyUserId = userId,
                    People = vm.People,
                    Name = User.Identity.Name,
                    //xtra Security getting price from database
                    TotalCost = vm.People * tp.PackagePrice,
                    TravelPackageName = vm.TravelPackageName,
                    VoucherCode = voucherCode,
                    LeftFeedback = false
                };
                _bookingRepo.Create(booking);
                //Send Email
                MyUser user = await _userManager.FindByIdAsync(userId);
                _emailService.SendEmail("grandetravelproject@gmail.com", user.Email, "Your Booking Voucher",
                            "Booking Date : " + booking.BookingDate + "\n" +
                            "Package Name : " + booking.TravelPackageName + "\n" +
                            "Number of People: " + booking.People + "\n" +
                            "Total cost : $" + booking.TotalCost + "\n" +
                            "Expiry Date : " + booking.BookingDate.AddMonths(3) + "\n" +
                            "Voucher Code : " + voucherCode);


                return RedirectToAction("Details", "TravelPackage", new { id = booking.TravelPackageId });
            }
            return View(vm);
        }
        [Route("PaymentRecieved/{id}")]
        [HttpGet]
        [Authorize]
        public IActionResult PaymentRecieved(int id)
        {
            Booking booking = _bookingRepo.GetSingle(b => b.BookingId == id);
            booking.PaymentReceived = true;
            _bookingRepo.Update(booking);
            return RedirectToAction("Index");
        }



        //[HttpGet]
        //[Authorize]
        //public IActionResult BraintreePayment(int id)
        //{
        //    var gateway = new BraintreeGateway
        //    {
        //        Environment = Braintree.Environment.SANDBOX,
        //        MerchantId = "t4xzfjwmtbd7dwvh",
        //        PublicKey = "s4ynn5bwvf4sc588",
        //        PrivateKey = "5dcc19cb0bcdbb3c3f24421a9f07961f"
        //    };
        //    var clientToken = gateway.ClientToken.generate();
        //    return View();

        //}

        //[HttpPost]
        //public ActionResult CreatePurchase(FormCollection collection)
        //{
        //    string nonceFromTheClient = collection["payment_method_nonce"];
        //    // Use payment method nonce here
        //}
    }
}
