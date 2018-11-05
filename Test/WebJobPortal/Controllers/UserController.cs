using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebJobPortal.Models;

namespace WebJobPortal.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(UserModel user)
        {
            return View();
        }
        
        public ActionResult Login(UserModel user)
        {
            return View();
        }
        public ActionResult UserProfile(UserModel user)
        {
            return View();
        }
        public ActionResult UserPartialView()
        {
            return PartialView();
        }
    }
}