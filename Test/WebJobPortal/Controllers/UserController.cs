using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebJobPortal.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {

        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}