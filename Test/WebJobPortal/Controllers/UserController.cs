
using JobPortal.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebJobPortal.Models;
using WebJobPortal.OfferReference;
using WebJobPortal.UserServiceReference;

namespace WebJobPortal.Controllers
{
    public class UserController : Controller
    {
        private const int lenght = 8;
        private const int n = 100000000;
        private readonly IUserService _proxy = new UserServiceClient("UserServiceHttpEndpoint");
        private readonly IOfferService _offerProxy = new OfferServiceClient("offerService");
        private IEnumerable<ServiceOfferWebModel> _serviceOffers = null;

        public UserController(IUserService proxy)
        {
            this._proxy = proxy;

        }
        public UserController()
        {


        }

    }
}
/*
        [HttpGet]
        public ActionResult UserProfile(UserServicesViewModel um)
        {
            // var tuple = new Tuple<UserModel, IEnumerable<ServiceOfferWebModel>>(um, _serviceOffers);
            um.User = new UserModel() { ID = 1 };
            var list = _offerProxy.GetAllOffers().Where(x => x.Author.ID == um.User.ID).Select(x => _mapper.Map(x, new ServiceOfferWebModel()));

            um.Services = (IEnumerable<WebJobPortal.Models.ServiceOfferWebModel>)list;
            // um.Services = new ServiceOfferWebModel[]{
            //     new ServiceOfferWebModel {
            //          Title = "Cleaning at you house",
            //          Description = "I'm very nice person, who'd love to clean your dirty socks",
            //          RatePerHour = 150 },
            //     new ServiceOfferWebModel {
            //         Title = "Gardening",
            //         Description = "I'm very nice person, who'd love to clean your garden",
            //         RatePerHour = 290 },
            //     new ServiceOfferWebModel {
            //         Title = "Graphic star",
            //         Description = "I'm very nice person, who'd love to  prepare logo for you",
            //         RatePerHour = 350 }, new ServiceOfferWebModel {
            //             Title = "Babysitter",
            //             Description = "I worked and au pair in NY for 3 months, then I was fired because I leart kid hhow to say f*ck",
            //             RatePerHour = 10 } };
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
            try
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
            catch
            {
                return View("Create");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var userDetails = this._proxy.DeleteUser(id);
                if (userDetails == false)
                {
                    return null;
                }
                else
                {
                    return RedirectToAction("Index", "ServiceOffer");
                }
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            User u = new User
            {
                ID = id,
                Email = Request.Form["Email"],
                Password = Request.Form["Password"],
                UserName = Request.Form["Username"],
                FirstName = Request.Form["firstName"],
                LastName = Request.Form["lastName"],
                Gender = (Gender)Enum.Parse(typeof(Gender), Request.Form["gender"]),
                PhoneNumber = Request.Form["phoneNumber"],
                AddressLine = Request.Form["addressLine"],
                Postcode = Request.Form["postcode"],
                CityName = Request.Form["cityName"],
                Region = (Region)Enum.Parse(typeof(Region), Request.Form["region"]),
            };

            bool result = this._proxy.EditUser(u);
            if (result)
            {
                return RedirectToAction("UserProfile", "User", new UserModel
                {
                    ID = u.ID,
                    Email = u.Email,
                    UserName = u.UserName,
                    Password = u.Password,
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
*/