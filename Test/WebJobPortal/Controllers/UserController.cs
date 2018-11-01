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
        } //Edit: Movie/Edit/id
        public ActionResult Edit( UserWebModel user)
        {
            return View();
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
    }
}