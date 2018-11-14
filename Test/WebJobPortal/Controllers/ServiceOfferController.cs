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
        public ActionResult Index()
        {
         // var list = _proxy.GetAllOffers();
         // IList toSend = new List<ServiceOfferWebModel>();
         // foreach (Offer offer in list)
         // {
         //     toSend.Add(_mapper.Map(offer, new ServiceOfferWebModel()));
         // }
         
            

            return View("View", new ServiceOfferWebModel[] { new ServiceOfferWebModel(), new ServiceOfferWebModel(), new ServiceOfferWebModel() });
        }
    }
}