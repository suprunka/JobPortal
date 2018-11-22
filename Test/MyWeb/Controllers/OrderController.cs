using JobPortal.Model;
using Microsoft.AspNet.Identity;
using MyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MyWeb.Controllers
{
    public class OrderController : Controller
    {
        private User u;
        private OfferReference.IOfferService _offerProxy;
        private UserServiceReference.IUserService _userProxy;
        private ShoppingCard shoppingCard;

        public OrderController()
        {
            _offerProxy = new OfferReference.OfferServiceClient("offerService");
            _userProxy = new UserServiceReference.UserServiceClient("UserServiceHttpEndpoint");
            
        }
        // GET: Order
        public ActionResult Index(string id)
        {
            u = _userProxy.FindUser(id);
            ShoppingCardView scv = new ShoppingCardView { Card = u.ShoppingCard };
            return View(scv);
        }

      
    }
}