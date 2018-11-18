using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Model;
using MyWeb.Models;
using AutoMapper;

namespace MyWeb.Controllers
{
    public class ServiceOfferController : Controller
    {

        private OfferReference.IOfferService _offerProxy = new OfferReference.OfferServiceClient("offerService");
        private IMapper _mapper;
        private IList<WorkingHours> workingdays = new List<WorkingHours>();


        // GET: Offer

        public ServiceOfferController 
()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<JobPortal.Model.Offer, ServiceOfferViewModel>();
            });

            _mapper = config.CreateMapper();

        }
        public ActionResult Index(string searchingString)
        {
            var list = _offerProxy.GetAllOffers().Select(x => _mapper.Map(x, new ManageOffers())).ToArray();
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));

            return View("Index", condition);
        }

        public ActionResult Add()
        {

            return View("Add", new Models.AddServiceOfferModel());


        }
        [HttpPost]
        public ActionResult Add(AddServiceOfferModel model)
        {
            bool result = _offerProxy.CreateServiceOffer(new Offer
            {
                Description = model.ManageOffers.Description,
                AuthorId = model.ManageOffers.Author,
                Title = model.ManageOffers.Title,
                RatePerHour = model.ManageOffers.RatePerHour,
                Category = model.ManageOffers.Category,
                Subcategory = model.ManageOffers.Subcategory,
                ListOfWorkingDays = workingdays.Select(x => _mapper.Map(x, new WorkingTime())).ToList(),
            });
            if(result)
                return View("Index");
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
                    })
                .ToList();
               
                return Json(subcats, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        public void AddtoArray(DayOfWeek day, int id)
        {

            string hour = id + ":00";
            string hourTo = id+1 + ":00";
            WorkingHours working = new WorkingHours { NameOfDay = day, HourFrom = TimeSpan.Parse(hour) , HourTo = TimeSpan.Parse(hourTo) };
            workingdays.Add(working);
            Console.WriteLine("" + day + "  " + hour + " " + hourTo);
        }

        public ActionResult ViewDetails()
        {
            return View("ViewDetails", new ManageOffers());
        }

        public ActionResult Edit(ManageOffers edited)
        {
            _offerProxy.UpdateServiceOffer(new Offer
            {
                Id = edited.Id,
                Title = edited.Title,
                Description = edited.Description,
                RatePerHour = edited.RatePerHour,
            });
            return View("ViewDetails", new ManageOffers());
        }
        public ActionResult Delete(int id)
        {
            if (_offerProxy.DeleteServiceOffer(id))
                return View("Index", "Home");
            return View("Index", "Home");


        }
    }
}