using Microsoft.AspNet.Identity;
using MyWeb.Mapping;
using MyWeb.OfferReference;
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
using MyWeb.UserReference1;

namespace MyWeb.Controllers
{
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private const int lenght = 8;
        private const int n = 100000000;
        private readonly IUserService _proxy;
        private readonly IOfferService _offerProxy;

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
        public UserController(IUserService proxy, IOfferService offerService)
        {
            this._proxy = proxy;
            this._offerProxy = offerService;
        }
        public UserController()
        {
            this._proxy = new UserServiceClient("UserServiceHttpEndpoint1");
            this._offerProxy = new OfferServiceClient("OfferServiceHttpEndpoint");
        }


        [HttpGet]
        public ActionResult UserProfile(string id)
        {
            UserProfileViewModel user = UserMapping.Map_User_To_UserProfileViewModel(_proxy.FindUser(id));
            user.Services = _offerProxy.GetAllOffers().Where(x => x.AuthorId == id).Select(x => new ManageOffers {
                Id = x.Id, Author = x.AuthorId, Description = x.Description, RatePerHour = x.RatePerHour, Title = x.Title,
                Subcategory = x.Subcategory, Category = x.Category }).ToArray();

            user.Bought = _offerProxy.GetAllBought(id).Select(x=> new BoughtOffers {
                Id = x.Id, Author = x.AuthorId, Description = x.Description, RatePerHour = x.RatePerHour,
                Title = x.Title, Subcategory = x.Subcategory, Category = x.Category, Date = x.WorkingDetails.Date,
                HourFrom = x.WorkingDetails.HoursFrom, HourTo = x.WorkingDetails.HoursTo}).ToArray();

            return View(user);
        }


        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            try
            {
                var isUserDeleted = await this._proxy.DeleteUserAsync((int)id);
                if (isUserDeleted == false)
                {
                    return null;
                }
                else
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return RedirectToAction("Index", "ServiceOffer");
                }
            }
            
            catch
            {
                return null;
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            return View(UserMapping.Map_User_To_UserProfileViewModel(await _proxy.FindUserByIDAsync((int) id)));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserProfileViewModel u)
        {
            if (ModelState.IsValid)
            {
                var isUpdated=  await _proxy.EditUserAsync(UserMapping.Map_UserProfileViewModel_To_User(u));
                if (isUpdated)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
                }
            }
            return View(u);
        }

        public async Task<ActionResult> AddDescription(int? id)
        {
            return View(UserMapping.Map_User_To_DescriptionViewModel(await _proxy.FindUserByIDAsync((int)id)));
        }

        [HttpPost]
        public async Task<ActionResult> AddDescription(DescriptionViewModel u)
        {
            string id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                var isUpdated = await _proxy.AddDescriptionAsync(UserMapping.Map_DescriptionViewModel_To_User(u));
                if (isUpdated)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId()});
                }
                return View(u);
            }
            else
            {
                return View(u);
            }
           

        }

        public async Task<ActionResult> ChangeEmail(int id)
        {
            return View(UserMapping.Map_User_To_ChangeEmailViewModel(await _proxy.FindUserByIDAsync(id)));
        }

        [HttpPost]
        public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid || model.NewEmail != null)
            {
                var isEmailChanged = await _proxy.EditUserEmailAsync(UserMapping.Map_ChangeEmailViewModel_To_User(model));
                if (isEmailChanged)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
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