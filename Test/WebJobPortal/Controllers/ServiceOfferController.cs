using AutoMapper;
using JobPortal.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebJobPortal.Models;
using WebJobPortal.OfferReference;

namespace WebJobPortal.Controllers
{
    public class ServiceOfferController : Controller
    {
        private IOfferService _proxy;
        private IMapper _mapper;
        public ServiceOfferController(IOfferService proxy)
        {
            _proxy = proxy;
        }
        public ServiceOfferController()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Offer, ServiceOfferWebModel>();
            });

            _mapper = config.CreateMapper();

            _proxy = new OfferReference.OfferServiceClient("offerService") ;
        }
        // GET: ServiceOffer
        public ActionResult Index(string searchingString)
        {
            // var list = _proxy.GetAllOffers();
            // IList toSend = new List<ServiceOfferWebModel>();
            // foreach (Offer offer in list)
            // {
            // toSend.Add(_mapper.Map(offer, new ServiceOfferWebModel()));
            // }
            // ServiceOfferWebModel[] list = new ServiceOfferWebModel[]{
            // new ServiceOfferWebModel {
            // Title = "Cleaning at you house",
            // Description = "I'm very nice person, who'd love to clean your dirty socks",
            // RatePerHour = 150 },
            // new ServiceOfferWebModel {
            // Title = "Gardening",
            // Description = "I'm very nice person, who'd love to clean your garden",
            // RatePerHour = 290 },
            // new ServiceOfferWebModel {
            // Title = "Graphic star",
            // Description = "I'm very nice person, who'd love to  prepare logo for you",
            // RatePerHour = 350 }, new ServiceOfferWebModel {
            // Title = "Babysitter",
            // Description = "I worked and au pair in NY for 3 months, then I was fired because I leart kid hhow to say f*ck",
            // RatePerHour = 10 },
            // new ServiceOfferWebModel
            //   {
            // Title = "Graphic star",
            // Description = "I'm very nice person, who'd love to  prepare logo for you",
            // RatePerHour = 350
            //   }, new ServiceOfferWebModel {
            // Title = "Babysitter",
            // Description = "I worked and au pair in NY for 3 months, then I was fired because I leart kid hhow to say f*ck",
            // RatePerHour = 10 },
            // new ServiceOfferWebModel
            //   {
            // Title = "Boring programmer",
            // Description = "Hello Word! I'm so excited and ready for new programming challenges! ",
            // RatePerHour = 300
            //   }, new ServiceOfferWebModel {
            // Title = "Bikeman",
            // Description = "Hi, I'm Marcin Marcin and I reapir bikes, I also buy them and sells for higher price :D",
            // RatePerHour = 10 } };

            var received = _proxy.GetAllOffers();
            IList list = new List<ServiceOfferWebModel>();
            foreach (var item in received)
            {
                list.Add(_mapper.Map(item, new ServiceOfferWebModel()));
            }
            
            if (searchingString == null)
               {
             return View("AllServices", list);
               }
            var condition = received.Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ServiceOfferWebModel()));
           
            return View("AllServices", condition);
        }
}
}