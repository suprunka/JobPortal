using JobPortal.Model;
using System;
using System.Net;
using System.Web.Mvc;
using WebJobPortal.Models;
using WebJobPortal.UserServiceReference;

namespace WebJobPortal.Controllers
{
    public class UserController : Controller
    {
        private const int lenght = 8;
        private const int n = 100000000;
        private readonly IUserService _proxy;

        public UserController(IUserService proxy)
        {
            this._proxy = proxy;
        }

        /*[HttpGet]
        public ActionResult Index(int? phoneNumber)
        {
            if (phoneNumber.HasValue && lenght == (int)(Math.Log10(n)))
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
                return View("UserProfile", um);
            }
            return View("Index");
        }*/

        [HttpGet]
        public ActionResult UserProfile(UserModel um)
        {
            return View(um);
        }

        //GET: User/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        //POST: Movie/Create
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                User u = new User
                {
                    ID = user.ID,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = user.Password,
                    AddressLine = user.AddressLine,
                    CityName = user.CityName,
                    Postcode = user.Postcode,
                    Region = user.Region,
                    Gender = user.Gender
                };
                _proxy.CreateUser(u);
                ModelState.Clear();
                ViewBag.SuccessMessage = "Creation done.";
                return View("Create", new UserModel());
            }
            else
            {
                return View("Create", user);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var userDetails = this._proxy.DeleteUser(id);
            if (userDetails == false)
            {
                return null;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            User u = new User
            {
                ID = id,
                FirstName = Request.Form["firstName"],
                LastName = Request.Form["lastName"],
                Gender = (Gender)Enum.Parse(typeof(Gender), Request.Form["gender"]),
                PhoneNumber = Request.Form["phoneNumber"],
                AddressLine = Request.Form["addressLine"],
                Postcode = Request.Form["postcode"],
                CityName = Request.Form["cityName"],
                Region = (Region)Enum.Parse(typeof(Region), Request.Form["region"]),
            };

            bool result = this._proxy.EditWebUser(u);
            if (result)
            {
                return RedirectToAction("UserProfile", "User", new UserModel
                {
                    ID = u.ID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Gender = u.Gender,
                    PhoneNumber = u.PhoneNumber,
                    AddressLine = u.AddressLine,
                    Postcode = u.Postcode,
                    CityName = u.CityName,
                    Region = u.Region
                });
            }
            else
            {
                return RedirectToAction("Search", "Home", Int32.Parse(u.PhoneNumber));
            }
        }  
    }
}