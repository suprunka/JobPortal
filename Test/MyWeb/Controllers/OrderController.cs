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
        private bool hasShoppingCard;

        public OrderController()
        {
            _offerProxy = new OfferReference.OfferServiceClient("offerService");
            _userProxy = new UserServiceReference.UserServiceClient("UserServiceHttpEndpoint");
            
            
            
        }
        // GET: Order
        public ActionResult Index(string id)
        {
            if (shoppingCard == null)
            {
                u = _userProxy.FindUser(id);
                shoppingCard = new ShoppingCard(u);
                shoppingCard.AddToCard(new OrderedOffer { Title = "siemka", WeekDay = DayOfWeek.Friday, RatePerHour = 30, HoursFrom = new TimeSpan(17, 0, 0), HoursTo = new TimeSpan(19, 0, 0), Description = "Elo" });
                shoppingCard.AddToCard(new OrderedOffer { Title = "ema", WeekDay = DayOfWeek.Monday, RatePerHour = 300, HoursFrom = new TimeSpan(12, 0, 0), HoursTo = new TimeSpan(22, 0, 0), Description = "Elo" });
                shoppingCard.AddToCard(new OrderedOffer { Title = "Dzik", WeekDay = DayOfWeek.Wednesday, RatePerHour = 910, HoursFrom = new TimeSpan(17, 0, 0), HoursTo = new TimeSpan(19, 0, 0), Description = "Elo" });
                //ShoppingCardView scv = new ShoppingCardView { Card = shoppingCard };

                return View();
            }
            else
            {
                return null;
            }
        }

      
    }
}