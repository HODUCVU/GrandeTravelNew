using GrandeTravel.Data;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandeTravel.Controllers
{
    [Route("Profile")]
    public class ProfileController : Controller
    {
        private UserManager<MyUser> _userManager;
        private IRepository<TravelProviderProfile> _travelProfileRepo;
        private IRepository<CustomerProfile> _customerProfileRepo;
        private IRepository<TravelPackage> _travelPackageRepo;

        public ProfileController(UserManager<MyUser> userManager, IRepository<TravelProviderProfile> travelProfileRepo, IRepository<CustomerProfile> customerProfileRepo, IRepository<TravelPackage> travelPackageRepo)
        {
            _travelProfileRepo = travelProfileRepo;
            _userManager = userManager;
            _customerProfileRepo = customerProfileRepo;
            _travelPackageRepo = travelPackageRepo;
        }

        // GET: /<controller>/
        [Route("UpdateProviderProfile")]
        [HttpGet]
        [Authorize(Roles = "TravelProvider")]
        public async Task<IActionResult> UpdateProviderProfile()
        {
            MyUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            TravelProviderProfile temp = _travelProfileRepo.GetSingle(p => p.UserId == user.Id);
            UpdateProviderProfileViewModel vm = new UpdateProviderProfileViewModel();
            if (temp != null)
            {

                vm.CompanyName = temp.CompanyName;
                vm.Phone = temp.Phone;

            }
            return View(vm);
        }
        [Route("UpdateProviderProfile")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "TravelProvider")]
        public async Task<IActionResult> UpdateProviderProfile(UpdateProviderProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //who logged in
                var loggedUser = await _userManager.FindByNameAsync(User.Identity.Name);
                //check if there is a profile already
                TravelProviderProfile loggedProfile = _travelProfileRepo.GetSingle(p => p.UserId == loggedUser.Id);
                //create or update
                if (loggedProfile != null)
                {

                    //update the loggedProfile
                    loggedProfile.CompanyName = vm.CompanyName;

                    loggedProfile.Phone = vm.Phone;
                    //save the update to the database
                    _travelProfileRepo.Update(loggedProfile);

                    //update existing Packages For Name Changes
                    IEnumerable<TravelPackage> list = _travelPackageRepo.Query(l => l.MyUserId == loggedUser.Id).ToList();
                    foreach (var item in list)
                    {
                        item.ProviderName = vm.CompanyName;
                        _travelPackageRepo.Update(item);
                    }
                }
                else
                {
                    //create new profile
                    loggedProfile = new TravelProviderProfile
                    {
                        UserId = loggedUser.Id,
                        CompanyName = vm.CompanyName,

                        Phone = vm.Phone
                    };
                    //save the new profile to database
                    _travelProfileRepo.Create(loggedProfile);

                }
                return RedirectToAction("Index", "TravelPackage");
            }

            return View(vm);

        }

        [Route("UpdateCustomerProfile")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerProfile()
        {
            MyUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            CustomerProfile temp = _customerProfileRepo.GetSingle(p => p.UserId == user.Id);
            UpdateCustomerProfileViewModel vm = new UpdateCustomerProfileViewModel();
            if (temp != null)
            {

                vm.FirstName = temp.FirstName;
                vm.LastName = temp.LastName;

                vm.Phone = temp.Phone;

            }
            return View(vm);
        }
        [Route("UpdateCustomerProfile")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerProfile(UpdateCustomerProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //who logged in
                var loggedUser = await _userManager.FindByNameAsync(User.Identity.Name);
                //check if there is a profile already
                CustomerProfile loggedProfile = _customerProfileRepo.GetSingle(p => p.UserId == loggedUser.Id);
                //create or update
                if (loggedProfile != null)
                {
                    //update the loggedProfile
                    loggedProfile.FirstName = vm.FirstName;
                    loggedProfile.LastName = vm.LastName;

                    loggedProfile.Phone = vm.Phone;
                    //save the update to the database
                    _customerProfileRepo.Update(loggedProfile);

                }
                else
                {
                    //create new profile
                    loggedProfile = new CustomerProfile
                    {
                        UserId = loggedUser.Id,
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        Phone = vm.Phone
                    };
                    //save the new profile to database
                    _customerProfileRepo.Create(loggedProfile);
                }
                return RedirectToAction("Index", "TravelPackage");
            }

            return View(vm);

        }
    }
}
