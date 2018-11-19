using Microsoft.AspNet.Identity;
using MyWeb.Mapping;
using MyWeb.OfferReference;
using MyWeb.UserServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebJobPortal.Models;
using JobPortal.Model;
using WebJobPortal;
using WebJobPortal.Controllers;
using AutoMapper;
using MyWeb.Models;


namespace MyWeb.Controllers
{
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private const int lenght = 8;
        private const int n = 100000000;
        private readonly IUserService _proxy = new UserServiceClient("UserServiceHttpEndpoint");
        private readonly IOfferService _offerProxy = new OfferServiceClient("offerService");
        private IEnumerable<ManageOffers> _serviceOffers = null;

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        public UserController(IUserService proxy)
        {
            this._proxy = proxy;
            
        }
        public UserController()
        {
           
        }


        [HttpGet]
        public ActionResult UserProfile(string id)
        {
            UserProfileViewModel user = UserMapping.Map_User_To_UserProfileViewModel(_proxy.FindUser(id));
            user.Services  = _offerProxy.GetAllOffers().Where(x => x.AuthorId== id).Select(x => new ManageOffers {Id = x.Id, Author=x.AuthorId, Description = x.Description, RatePerHour = x.RatePerHour, Title = x.Title , Subcategory = x.Subcategory, Category = x.Category}).ToArray();

            return View(user);
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
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return null;
            }
        }
        

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(UserMapping.Map_User_To_UserProfileViewModel(_proxy.FindUserByID(id)));
        }

        [HttpPost]
        public ActionResult Edit(UserProfileViewModel u)
        {
            if (ModelState.IsValid)
            {
               var isUpdated=  _proxy.EditUser(UserMapping.Map_UserProfileViewModel_To_User(u));
                if (isUpdated)
                {
                    return UserProfile(User.Identity.GetUserId());
                }
            }
            return View(u);

        }

        
        public ActionResult AddDescription(int id)
        {
            return View(UserMapping.Map_User_To_DescriptionViewModel(_proxy.FindUserByID(id)));
        }

        [HttpPost]
        public ActionResult AddDescription(DescriptionViewModel u)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _proxy.AddDescription(UserMapping.Map_DescriptionViewModel_To_User(u));
                if (isUpdated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(u);

        }




        public ActionResult ChangeEmail(int id)
        {
            return View(UserMapping.Map_User_To_ChangeEmailViewModel(_proxy.FindUserByID(id)));
        }

        [HttpPost]
        public ActionResult ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var changeEmail = _proxy.EditUserEmail(UserMapping.Map_ChangeEmailViewModel_To_User(model));
                if (changeEmail)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}