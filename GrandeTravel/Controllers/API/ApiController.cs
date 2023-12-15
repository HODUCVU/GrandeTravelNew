using GrandeTravel.Data;
using GrandeTravel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GrandeTravel.Controllers.API
{
    [Route("api")]
    public class ApiController : Controller
    {
        private IRepository<TravelPackage> _travelPackageRepo;

        public ApiController(IRepository<TravelPackage> travelPackageRepo)
        {
            _travelPackageRepo = travelPackageRepo;
        }
        [HttpGet("getAll")]
        public JsonResult GetAll()
        {
            try
            {
                var list = _travelPackageRepo.Query(p => !p.Discontinued);
                AllPackages packages = new AllPackages
                {
                    packages = list
                };
                return Json(packages);
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }

        [HttpGet("getAllangular")]
        public JsonResult GetAllangular()
        {
            try
            {
                var list = _travelPackageRepo.Query(p => !p.Discontinued);

                return Json(list);
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }

        [HttpPost("PackageByDesc")]
        public JsonResult SearchPackageDesc(string description)
        {
            List<TravelPackage> list = new List<TravelPackage>();
            string[] searchDescription = description.Split(' ');
            try
            {
                foreach (var item in searchDescription)
                {
                    var tempList = _travelPackageRepo.Query(c => c.PackageDescription.Contains(item) && !c.Discontinued);
                    foreach (var package in tempList)
                    {
                        list.Add(package);
                    }

                }
                list = list.Distinct().ToList();
                return Json(list);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }

        [HttpPost("SearchCombine")]
        public JsonResult SearchCombine(string description, string location)
        {

            List<TravelPackage> list = new List<TravelPackage>();
            string[] searchDescription;
            string[] locations;
            try
            {
                if (!String.IsNullOrEmpty(location) && !String.IsNullOrEmpty(description))
                {

                    locations = location.Split(',');
                    searchDescription = description.Split(' ');
                    foreach (var item in locations)
                    {
                        var tempList = _travelPackageRepo.Query(c => c.Location.Contains(item) && !c.Discontinued);
                        foreach (var package in tempList)
                        {
                            foreach (var search in searchDescription)
                            {
                                if (package.PackageDescription.Contains(search))
                                {
                                    list.Add(package);
                                }
                            }

                        }
                    }
                    //list = list.Distinct().ToList();
                    //return Json(list);
                }
                else if (!String.IsNullOrEmpty(description))
                {

                    searchDescription = description.Split(' ');
                    foreach (var item in searchDescription)
                    {
                        var tempList = _travelPackageRepo.Query(c => c.PackageDescription.Contains(item) && !c.Discontinued);
                        foreach (var package in tempList)
                        {
                            list.Add(package);
                        }

                    }
                    list = list.Distinct().ToList();
                    //return Json(list);

                }
                else if (!String.IsNullOrEmpty(location))
                {
                    locations = location.Split(',');
                    foreach (var item in locations)
                    {
                        var tempList = _travelPackageRepo.Query(c => c.Location.Contains(item) && !c.Discontinued);
                        foreach (var package in tempList)
                        {
                            list.Add(package);
                        }


                    }


                }
                return Json(list);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }

        //[HttpPost("api/PackageByLocation")]
        //public JsonResult SearchPackage(string location)
        //{
        //    List<TravelPackage> list = new List<TravelPackage>();
        //    if (!String.IsNullOrEmpty(location))
        //    {
        //        string[] locations = location.Split(',');
        //        try
        //        {

        //            foreach (var item in locations)
        //            {
        //                var tempList = _travelPackageRepo.Query(c => c.Location.Contains(item) && !c.Discontinued);
        //                foreach (var package in tempList)
        //                {
        //                    list.Add(package);
        //                }


        //            }

        //            return Json(list);
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            return Json(new { Message = ex.Message });
        //        }
        //    }
        //    return Json(list);

        //}

        [HttpGet("getLocations")]
        public JsonResult GetLocations()
        {
            try
            {
                var list = _travelPackageRepo.Query(p => !p.Discontinued).Select(p => p.Location).Distinct();
                return Json(list);
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }
    }
}
