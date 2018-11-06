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

namespace WebJobPortal.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _proxy;
        public UserController()//, IMapper mapper)
        {
            //_proxy = proxy;
        }
        public UserController(IUserService proxy)//, IMapper mapper)
    {
        _proxy = proxy;
    }

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

                  //  _proxy.CreateUser(AutoMapper.Mapper.Map(user, new JobPortal.Model.User()));
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Create",user);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null || !id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userDetails = this._proxy.FindUser((int)id);

            if (userDetails == null)
                return HttpNotFound();

            UserModel userToDelete = new UserModel {UserName=userDetails.UserName, Email=userDetails.Email};

            return View("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null || !id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userDetails = this._proxy.FindUser((int)id);

            if (userDetails == null)
                return HttpNotFound();

            var userToEdit = new UserModel { UserName = userDetails.UserName, Email = userDetails.Email};

            ViewBag.Title = "Edit: " + userToEdit.UserName;

            return View(userToEdit);

        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(user);

                //this._proxy.EditUser(new MovieResource { Id = movie.Id, Title = movie.Title, Description = movie.Description });

                return RedirectToAction("Index");
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