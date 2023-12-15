using GrandeTravel.Data;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandeTravel.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private UserManager<MyUser> _userManager;
        private SignInManager<MyUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private IEmailSender _emailService;
        public AccountController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager, 
            RoleManager<IdentityRole> roleManager, IEmailSender emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel vm)
        {

            if (ModelState.IsValid)
            {
                MyUser tempUser = new MyUser
                {
                    UserName = vm.Username,
                    Email = vm.Email

                };
                ///admin check
                IEnumerable<MyUser> admin = await _userManager.GetUsersInRoleAsync("Admin");

                var result = await _userManager.CreateAsync(tempUser, vm.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(tempUser);

                    var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account",
                        new { userId = tempUser.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    _emailService.SendEmail("hoducvu1234@gmail.com", vm.Email, "Confirm Registration",
                        $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");



                    //remember to add roles first!!
                    if (admin.Count() == 0)
                    {
                        await _userManager.AddToRoleAsync(tempUser, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(tempUser, "Customer");
                    }

                    //await _signInManager.SignInAsync(tempUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Route("RegisterTravelProvider")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterTravelProvider()
        {
            return View();
        }
        [Route("RegisterTravelProvider")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterTravelProvider(RegisterUserViewModel vm)
        {

            if (ModelState.IsValid)
            {
                MyUser tempUser = new MyUser
                {
                    UserName = vm.Username,
                    Email = vm.Email

                };
                var result = await _userManager.CreateAsync(tempUser, vm.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(tempUser);

                    var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account",
                        new { userId = tempUser.Id, code = code }, protocol: HttpContext.Request.Scheme);

                   _emailService.SendEmail("hoducvu1234@gmail.com", vm.Email, "Confirm Registration",
                        $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                    //remember to add roles first!!
                    await _userManager.AddToRoleAsync(tempUser, "TravelProvider");
                    // await _signInManager.SignInAsync(tempUser,false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        [Route("LogIn")]
        [HttpGet]
        public IActionResult LogIn(string returnUrl = "")
        {
            LoginViewModel vm = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }
        [Route("LogIn")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(vm.Username);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty,
                                      "You must have a confirmed email to log in.");
                        return View(vm);
                    }
                }
                var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Username or Password Incorrect");
            /*return View(vm);*/
            return BadRequest(ModelState);
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("AddRole")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }
        [Route("AddRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(AddRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //check if role exists
                var theRole = await _roleManager.FindByNameAsync(vm.NewRole);
                if (theRole == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(vm.NewRole));
                    //change later to redirect to display roles
                    return RedirectToAction("Index", "TravelPackage");
                }
                else
                {
                    ModelState.AddModelError("", "Role Name already used");
                }
            }
            return View(vm);
        }
        [Route("ConfirmEmail")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [Route("TravelProviderList")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TravelProviderList()
        {
            IEnumerable<MyUser> travelProviders = await _userManager.GetUsersInRoleAsync("TravelProvider");

            DisplayAllTravelProvidersViewModel vm = new DisplayAllTravelProvidersViewModel
            {
                TravelProviders = travelProviders
            };

            return View(vm);
        }

        [Route("CustomerList")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CustomerList()
        {
            IEnumerable<MyUser> customers = await _userManager.GetUsersInRoleAsync("Customer");

            DisplayAllCustomersViewModel vm = new DisplayAllCustomersViewModel
            {
                Customers = customers
            };

            return View(vm);
        }
    }
}
