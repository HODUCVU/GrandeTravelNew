using GrandeTravel.Data;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandeTravel.Controllers
{
    [Route("Photo")]
    public class PhotoController : Controller
    {
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _TravelPackageRepo;

        private IWebHostEnvironment _HostingEnviro;

        private IRepository<Photo> _photoRepo;

        public PhotoController(IRepository<TravelPackage> TravelPackagerepo, IWebHostEnvironment HostingEnviro, UserManager<MyUser> userManager, IRepository<Photo> photoRepo)
        {
            _photoRepo = photoRepo;
            _TravelPackageRepo = TravelPackagerepo;
            _HostingEnviro = HostingEnviro;
            _userManager = userManager;

        }
        // GET: /<controller>/
        [Route("Create")]
        [HttpGet]
        [Authorize(Roles = "TravelProvider")]
        public IActionResult Create(int id)
        {
            CreatePhotoViewModel vm = new CreatePhotoViewModel
            {
                TravelPackageId = id
            };
            return View(vm);
        }
        [Route("Create")]
        [HttpPost]
        [Authorize(Roles = "TravelProvider")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePhotoViewModel vm, IList<IFormFile> PhotoLocation)
        {
            if (ModelState.IsValid)
            {
                if (PhotoLocation != null)
                {
                    int count = _photoRepo.Query(p => p.TravelPackageId == vm.TravelPackageId).Count() + 2;
                    if ((count - 2) + PhotoLocation.Count() > 4)
                    {
                        ModelState.AddModelError("PhotoLocation", "Cannot have more than 5 Photos Per Package!");
                        return View(vm);
                    }
                    TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == vm.TravelPackageId);
                    foreach (var item in PhotoLocation)
                    {
                        string uploadPath = Path.Combine(_HostingEnviro.WebRootPath, "Media\\TravelPackage");
                        string filename = User.Identity.Name + "-" + tp.PackageName + "-" + count + Path.GetExtension(item.FileName);
                        uploadPath = Path.Combine(uploadPath, filename);
                        using (FileStream fs = new FileStream(uploadPath, FileMode.Create))
                        {
                            item.CopyTo(fs);
                        }
                        string SaveFilename = Path.Combine("Media\\TravelPackage", filename);
                        Photo tempphoto = new Photo
                        {
                            PhotoLocation = SaveFilename,
                            TravelPackageId = vm.TravelPackageId
                        };
                        _photoRepo.Create(tempphoto);
                        count++;
                    }
                    return RedirectToAction("Details", "TravelPackage", new { id = vm.TravelPackageId });
                }

            }
            return View(vm);
        }
    }
}
