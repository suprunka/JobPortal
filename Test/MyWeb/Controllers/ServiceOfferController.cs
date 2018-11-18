using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Model;
using MyWeb.Models;

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
            var list = _offerProxy.GetAllOffers().Select(x => _mapper.Map(x, new ServiceOfferViewModel())).ToArray();
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ServiceOfferViewModel()));

            return View("Index", condition);
        }

        public ActionResult Add()
        {

            return View("Add", new Models.AddServiceOfferModel());


        }
        public JsonResult GetSubCategoryList(string category)
        {

            IList<SubCategory> final = new List<SubCategory>();
            //  List<State> StateList = db.States.Where(x => x.CountryId == CountryId).ToList();
            IEnumerable<JobPortal.Model.SubCategory> subcategories = Enum.GetValues(typeof(JobPortal.Model.SubCategory)).Cast<JobPortal.Model.SubCategory>();
            foreach (JobPortal.Model.SubCategory subcat in subcategories)
            {
                if (subcat.IsSubcategoryOf((Category)Enum.Parse(typeof(Category), category)))
                {
                    final.Add(subcat);
                }
            }
            return Json(final, JsonRequestBehavior.AllowGet);


        }
        public void AddtoArray(DayOfWeek day, int id)
        {
            string hour = id + ":00";
            string hourTo = id+1 + ":00";
            WorkingHours working = new WorkingHours { NameOfDay = day, HourFrom = TimeSpan.Parse(hour) , HourTo = TimeSpan.Parse(hourTo) };
            workingdays.Add(working);
            Console.WriteLine("" + day + "  " + hour + " " + hourTo);
        }
    }
}