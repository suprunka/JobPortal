using AutoMapper;
using ServiceLibrary;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebJobPortal.Models;
using WebJobPortal.Validator;

namespace WebJobPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _proxy;

        public UserController(IUserService proxy)//, IMapper mapper)
        {
            _proxy = proxy;


        }
        // GET: Users
        public ActionResult Index(string id)
        {
            return View();
        }

        //Get: Movie/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        //POST: Movie/Create
        [HttpPost]
        public ActionResult Create(UserWebModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _proxy.CreateUser(AutoMapper.Mapper.Map(user, new Users()));
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


        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null || !id.HasValue)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var foundUser = _proxy.FindUser((int)id);
                if (foundUser == null)
                {
                    return HttpNotFound();
                }
                UserWebModel u = Mapper.Map(foundUser, new UserWebModel());
                return View(u);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        public ActionResult DeleteConfirm(UserWebModel u)
        {
            try
            {
                if (u == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                _proxy.DeleteUser(u.ID);
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

        }


        public ActionResult Edit(UserWebModel u)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                _proxy.EditUser(Mapper.Map(u, new Users()));
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}