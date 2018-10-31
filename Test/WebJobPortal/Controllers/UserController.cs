using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebJobPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _proxy;

        public UserController(IUserService proxy)
        {
            _proxy = proxy;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            
        }

        public ActionResult Create(UserModel user)
        {

        }
    }
}