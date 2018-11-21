using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebJobPortal.Models;
using WebJobPortal.UserServiceReference;

namespace WebJobPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _proxy;
        private const int lenght = 8;
        private const int n = 100000000;

        public HomeController(IUserService proxy)
        {
            this._proxy = proxy;
        }

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult Search(int? phoneNumber)
        {
            try
            {
                if (phoneNumber.HasValue && phoneNumber.ToString().Length == (int)(Math.Log10(n)))
                {
                    string pn = phoneNumber.ToString();
                    var found = _proxy.FindUser(pn);
                    UserModel um = new UserModel
                    {
                        ID = found.ID,
                        PhoneNumber = found.PhoneNumber,
                        FirstName = found.FirstName,
                        LastName = found.LastName,
                        Email = found.Email,
                        UserName = found.UserName,
                        Password = found.Password,
                        AddressLine = found.AddressLine,
                        CityName = found.CityName,
                        Postcode = found.Postcode,
                        Region = found.Region,
                        Gender = found.Gender
                    };
                    return RedirectToAction("UserProfile", "User", um);
                }
                return View("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}