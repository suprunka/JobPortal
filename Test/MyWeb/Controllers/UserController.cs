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
using WebJobPortal;
using WebJobPortal.Controllers;
using AutoMapper;
using MyWeb.UserReference1;
using MyWeb.OrderReference;
using WebJobPortal.Models;
using PagedList;

namespace MyWeb.Controllers
{
    public class UserController : Controller
    {
        private IAuthenticationManager _authnManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserService _proxy;
        private readonly IOfferService _offerProxy;
        private readonly IOrderService _orderProxy;

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
        public UserController(IUserService proxy, IOfferService offerService, IOrderService orderProxy)
        {
            this._proxy = proxy;
            this._offerProxy = offerService;
            this._orderProxy = orderProxy;
        }

        public UserController(IUserService proxy, IOfferService offerService, IOrderService orderProxy, IAuthenticationManager authentication)
        {
            this._proxy = proxy;
            this._offerProxy = offerService;
            this._orderProxy = orderProxy;
            this._authnManager = authentication;
        }



        public UserController()
        {
            this._proxy = new UserServiceClient("UserServiceHttpEndpoint1");
            this._offerProxy = new OfferServiceClient("OfferServiceHttpEndpoint");
            this._orderProxy = new OrderReference.OrderServiceClient("OrderServiceHttpEndpoint");
        }


        [HttpGet]
        public async Task<ActionResult> UserProfile(string id, DateTime? date = null)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }

            UserProfileViewModel user = Mapping.Mapping.Map_User_To_UserProfileViewModel(_proxy.FindUser(id));
            var allOffers = await _offerProxy.GetAllOffersAsync();
            var userServices = allOffers.Where(x => x.AuthorId == id);
            user.Services = userServices.Select(y => Mapping.Mapping.Map_Offer_To_ManageOffers(y)).ToPagedList(1, userServices.Count() + 1);
            var allBought = await _offerProxy.GetAllBoughtAsync(id);
            user.Bought = allBought.Select(x => Mapping.Mapping.Map_Offer_To_BoughtOffers(x)).ToArray();

            user.Date = (DateTime)date;

            var jobCalendar = await _orderProxy.GetJobCallendarAsync((DateTime)date, id);
            user.Jobs = jobCalendar.Select(x =>
            Mapping.Mapping.Map_JobOffer_JPModel_To_WebJobPortal_JobOffer(x, (DateTime)date, user)).ToArray();

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
            return View(Mapping.Mapping.Map_User_To_UserProfileViewModel(await _proxy.FindUserByIDAsync((int)id)));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserProfileViewModel u)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = await _proxy.EditUserAsync(Mapping.Mapping.Map_UserProfileViewModel_To_User(u));
                if (isUpdated)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
                }
                else
                {
                    TempData["msg"] = "<script>alert('User account information has been changed before your changes, check new data before you update ');</script>";

                    return RedirectToAction("UserProfile", "Edit", new { id = User.Identity.GetUserId() });
                }
            }
            return View(u);
        }

        public async Task<ActionResult> AddDescription(int? id)
        {
            return View(Mapping.Mapping.Map_User_To_DescriptionViewModel(await _proxy.FindUserByIDAsync((int)id)));
        }

        [HttpPost]
        public async Task<ActionResult> AddDescription(ChangeDescriptionViewModel u)
        {
            string id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                var isUpdated = await _proxy.AddDescriptionAsync(Mapping.Mapping.Map_DescriptionViewModel_To_User(u));
                if (isUpdated)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
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
            return View(Mapping.Mapping.Map_User_To_ChangeEmailViewModel(await _proxy.FindUserByIDAsync(id)));
        }

        [HttpPost]
        public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid || model.NewEmail != null)
            {
                var isEmailChanged = await _proxy.EditUserEmailAsync(Mapping.Mapping.Map_ChangeEmailViewModel_To_User(model));
                if (isEmailChanged)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
                }
            }
            return View(model);
        }

        #region Helpers
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
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (_authnManager == null)
                {
                    return HttpContext.GetOwinContext().Authentication;
                }
                return _authnManager;
            }
        }
        #endregion
    }
}