using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Model;
using MyWeb.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using MyWeb.OfferReference;

namespace MyWeb.Controllers
{
    public class ServiceOfferController : Controller
    {

        private OfferReference.IOfferService _offerProxy;
        private OrderReference.IOrderService _orderProxy;
        private IMapper _mapper;
        private IList<WorkingHours> workingdays = new List<WorkingHours>();


        // GET: Offer

        public ServiceOfferController()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<JobPortal.Model.Offer, ManageOffers>();
            });

            _mapper = config.CreateMapper();
            _offerProxy = new OfferReference.OfferServiceClient("OfferServiceHttpEndpoint");
            _orderProxy = new OrderReference.OrderServiceClient("OrderServiceHttpEndpoint");
            
        }
        public ServiceOfferController(IOfferService proxy)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<JobPortal.Model.Offer, ManageOffers>();
            });
            _mapper = config.CreateMapper();
            _offerProxy = proxy;
        }
        public ActionResult Index(string searchingString)
        {
            var list =  _offerProxy.GetAllOffers().Select(x => _mapper.Map(x, new ManageOffers())).ToArray();
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }

        public ActionResult Add()
        {

            return View("Add", new Models.AddServiceOfferModel());


        }
        [HttpPost]
        public async Task<ActionResult> Add(AddServiceOfferModel model)
        {
            bool result = await _offerProxy.CreateServiceOfferAsync(new Offer
            {
                Description = model.ManageOffers.Description,
                AuthorId =  User.Identity.GetUserId(),
                Title = model.ManageOffers.Title,
                RatePerHour = model.ManageOffers.RatePerHour,
                Category = model.ManageOffers.Category,
                Subcategory = model.ManageOffers.Subcategory,
                //ListOfWorkingDays = workingdays.Select(x => _mapper.Map(x, new WorkingTime())).ToList(),
            });
            if(result)
                return RedirectToAction("Index");
            return View("Add", model);


        }
        [HttpGet]
        public ActionResult GetSubCategories(string iso3)
        {
            if (!string.IsNullOrWhiteSpace(iso3))
            {
                var repo = Enum.GetValues(typeof(SubCategory));
                var myenum = (Category)Enum.Parse(typeof(Category), iso3.ToString(), true);
                IEnumerable<SelectListItem> subcats = Enum.GetValues(typeof(SubCategory)).Cast<SubCategory>().Where(x => x.IsSubcategoryOf(myenum)).Cast<SubCategory>().Select(p => new SelectListItem()
                {
                    Text = p.ToString(),
                    Value = p.ToString()
                }).ToList();
                return Json(subcats, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetHoursFrom(int serviceId, DateTime date)
        {
            try
            {
                IEnumerable<SelectListItem> hoursFrom = _orderProxy.GetHoursFrom(serviceId, date).Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Value = x.ToString()
                }).ToList();

                return Json(hoursFrom, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpGet]
        public ActionResult GetHoursTo(int serviceId, DateTime date,TimeSpan from)
        {

            IEnumerable<SelectListItem> hoursto = _orderProxy.GetHoursTo(serviceId, date, from).Select(x => new SelectListItem()
            {
                Text = x.ToString(),
                Value = x.ToString()
            })
                .ToList();

            return Json(hoursto, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public async Task<ActionResult> ViewDetails(int  id)
        {
            var found = await _offerProxy.FindServiceOfferAsync(id);
            var foundDates = _offerProxy.GetAllWorkingDays().Where(x => x.OfferId == id);
            ViewDetails model = new ViewDetails { Id = found.Id, Title = found.Title, Author = found.AuthorId, Description = found.Description, RatePerHour = found.RatePerHour, Dates = foundDates, Category = found.Category, Subcategory= found.Subcategory};
            return View( model);
        }

        [HttpPost]
        public ActionResult ViewDetails(ViewDetails edited)
        {
                var isUpdated = _offerProxy.UpdateServiceOffer(new Offer
                {
                    Id = edited.Id,
                    Title = edited.Title,
                    RatePerHour = edited.RatePerHour,
                    Description = edited.Description,
                    WorkingTimes = edited.Dates,
                });
                if (isUpdated)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
                }

            return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
        }

        public async Task<ActionResult> Delete(int idd)
        {
            var isDeleted = await _offerProxy.DeleteServiceOfferAsync(idd);
            if (isDeleted)
                return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
            return RedirectToAction("ViewDetails", "ServiceOffer", new { id = idd});



        }
    }
}