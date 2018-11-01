using ServiceLibrary;
using System;
using WebJobPortal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLibrary.Models;
//using WebJobPortal.ServiceReference1;

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
        public ActionResult Index(string SearchString)
        {
            return View();
        }
        public ActionResult UserList()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create(UserModel user)
        {

            return View();
        }
        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            throw new NotImplementedException();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            throw new NotImplementedException();

        }

    }
}