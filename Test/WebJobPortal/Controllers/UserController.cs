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
        public ActionResult Create()
        {
            return View();
        }
        //POST: Movie/Create

        public ActionResult Delete()
        {
            return View("UserProfile");
        }

        public ActionResult Edit()
        {
            return View("UserProfile");
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}