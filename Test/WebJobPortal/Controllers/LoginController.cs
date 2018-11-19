using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebJobPortal.Models;
using WebJobPortal.UserServiceReference;

namespace WebJobPortal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _proxy = new UserServiceClient("UserServiceHttpEndpoint");

        public LoginController(IUserService proxy)
        {
            this._proxy = proxy;
        }
        public LoginController()
        {

        }

        // GET: Login
        public PartialViewResult Index()
        {
            return PartialView("Login", new UserModel());
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginWebModel model)
        {
            if (ModelState.IsValid)
            {
                bool isAuthenticated = false;

                try
                {
                    using (var client = new UserServiceClient("UserServiceHttpEndpoint"))
                    {
          
                        var response = client.Login(model.Username, model.Password);
                        isAuthenticated = response;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                if (isAuthenticated)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    return RedirectToAction("UserProfile", "User");
                    

                    }
                else
                {
                    ModelState.AddModelError("", "Access Denied");
                    return Json("Access Denied");
                   // return View(model);

                    //redirect to Home page??
                }
            }
            return Json("Access Denied, ModelState invalid");
            //return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}
 