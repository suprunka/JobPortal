﻿using AutoMapper;
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
        // GET: User
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
        

        public ActionResult Delete(int? id)
        {
            if(id == null|| !id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var foundUser = _proxy.FindUser((int)id);
            if(foundUser == null)
            {
                return HttpNotFound();
            }

            User u = Mapper.Map(foundUser, new User());
            return View(u);

        }

        [HttpPost]
        public ActionResult DeleteConfirm(User u)
        {
            try
            {
                if(u==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                _proxy.DeleteUser(u.PhoneNumber);
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}