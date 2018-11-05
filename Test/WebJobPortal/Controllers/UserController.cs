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
        public //   


        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //POST: Movie/Create
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _proxy.CreateUser(AutoMapper.Mapper.Map(user, new User()));
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Create");
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult Login(UserModel user)
        {
            return View();
        }
        public ActionResult UserProfile(UserModel user)
        {
            return View();
        }
    }
}