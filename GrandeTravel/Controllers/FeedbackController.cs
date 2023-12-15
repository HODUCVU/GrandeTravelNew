using GrandeTravel.Data;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandeTravel.Controllers
{
    [Route("Feedback")]
    public class FeedbackController : Controller
    {
        private IRepository<Booking> _bookingRepo;
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _travelPackageManager;
        private IRepository<Feedback> _feedbackManager;


        public FeedbackController(IRepository<Booking> bookingRepo, UserManager<MyUser> userManager, IRepository<TravelPackage> travelPackageManager, IRepository<Feedback> feedbackManager)
        {
            _feedbackManager = feedbackManager;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _travelPackageManager = travelPackageManager;
        }
        // GET: /<controller>/
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Create/{id}")]
        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Create(int id)
        {
            Booking booking = _bookingRepo.GetSingle(t => t.BookingId == id);
            //add check for security
            if (booking != null && !booking.LeftFeedback)
            {
                if (booking.MyUserId == _userManager.GetUserId(User))
                {
                    CreateFeedbackViewModel vm = new CreateFeedbackViewModel
                    {
                        TravelPackageId = booking.TravelPackageId,
                        BookingId = booking.BookingId
                    };
                    return View(vm);
                }
            }

            return RedirectToAction("AccessDenied", "Account");

        }
        [Route("Create/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Create(CreateFeedbackViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Booking booking = _bookingRepo.GetSingle(t => t.BookingId == vm.BookingId);
                if (booking != null && !booking.LeftFeedback)
                {
                    var userId = _userManager.GetUserId(User);
                    Feedback newfeedback = new Feedback
                    {
                        UserName = User.Identity.Name,
                        TravelPackageId = vm.TravelPackageId,
                        MyUserId = userId,
                        Rating = vm.Rating,
                        Comment = vm.Comment
                    };
                    _feedbackManager.Create(newfeedback);
                    booking.LeftFeedback = true;
                    _bookingRepo.Update(booking);

                    return RedirectToAction("Details", "TravelPackage", new { id = newfeedback.TravelPackageId });
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Account");
                }

            }
            return View(vm);
        }
    }
}
