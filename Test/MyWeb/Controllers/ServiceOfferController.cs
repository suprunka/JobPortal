using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using MyWeb.OfferReference;
using PagedList;
using WebJobPortal.Models;
using JobPortal.Model;

namespace MyWeb.Controllers
{
    public class ServiceOfferController : Controller
    {

        private OfferReference.IOfferService _offerProxy;
        private UserReference1.IUserService _userProxy;
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
            _userProxy = new UserReference1.UserServiceClient("UserServiceHttpEndpoint1");


        }
        public ServiceOfferController(IOfferService proxy)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<JobPortal.Model.Offer, ManageOffers>();
            });
            _mapper = config.CreateMapper();
            _offerProxy = proxy;
        }
        public async Task<ActionResult> Index(string searchingString, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var all = await _offerProxy.GetAllOffersAsync();
            var list = all.OrderBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOffers())).ToPagedList(pageIndex, 12);
            if (searchingString == null)
            {
                return View(list);
            }
            var condition = list.OrderBy(x => x.RatePerHour).Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }

        public async Task<ActionResult> Home(string searchingString, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var all = await _offerProxy.GetAllOffersAsync();
            var list = all.Where(x => x.Category.ToString() == "Home").OrderBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOffers())).ToPagedList(pageIndex, 12);
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.OrderBy(x => x.RatePerHour).Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }

        public async Task<ActionResult> Tutoring(string searchingString, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var all = await _offerProxy.GetAllOffersAsync();
            var list = all.Where(x => x.Category.ToString() == "Tutoring").OrderBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOffers())).ToPagedList(pageIndex, 12);
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.OrderBy(x => x.RatePerHour).Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }

        public async Task<ActionResult> IT(string searchingString, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var all = await _offerProxy.GetAllOffersAsync();
            var list = all.Where(x => x.Category.ToString() == "IT").OrderBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOffers())).ToPagedList(pageIndex, 12);
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.OrderBy(x => x.RatePerHour).Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }

        public async Task<ActionResult> Repairs(string searchingString, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var all = await _offerProxy.GetAllOffersAsync();
            var list = all.Where(x => x.Category.ToString() == "Repairs").OrderBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOffers())).ToPagedList(pageIndex, 12);
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.OrderBy(x => x.RatePerHour).Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }

        public async Task<ActionResult> Architecture(string searchingString, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var all = await _offerProxy.GetAllOffersAsync();
            var list = all.Where(x => x.Category.ToString() == "Architecture").OrderBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOffers())).ToPagedList(pageIndex, 12);
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.OrderBy(x => x.RatePerHour).Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }

        public async Task<ActionResult> Media(string searchingString, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var all = await _offerProxy.GetAllOffersAsync();
            var list = all.Where(x => x.Category.ToString() == "Media").OrderBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOffers())).ToPagedList(pageIndex, 12);
            if (searchingString == null)
            {
                return View("Index", list);
            }
            var condition = list.OrderBy(x => x.RatePerHour).Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOffers()));
            return View("Index", condition.ToArray());
        }


        public ActionResult Add()
        {

            return View("Add", new AddServiceOfferModel());


        }
        [HttpPost]
        public async Task<ActionResult> Add(AddServiceOfferModel model)
        {
            bool result = await _offerProxy.CreateServiceOfferAsync(
                Mapping.Mapping.Map_AddServiceOfferModel_To_Offer(model, User.Identity.GetUserId()));
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
        public async Task<ActionResult> GetHoursTo(int serviceId, DateTime date,TimeSpan from)
        {

            var hoursto = await _orderProxy.GetHoursToAsync(serviceId, date, from);
            hoursto.Select(x => new SelectListItem()
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
            ReviewModel[] reviewstomodel = null;
            var found = await _offerProxy.FindServiceOfferAsync(id);
            var foundDates = _offerProxy.GetAllWorkingDays().Where(x => x.OfferId == id);
            var reviews = _offerProxy.GetServiceReviews(id);
            if (reviews != null)
                
            reviewstomodel= reviews.Select(x => new ReviewModel { Customer = new ReviewAuthor {Gender = _userProxy.FindUser(x.CustomerId).Gender, Username = _userProxy.FindUser(x.CustomerId).UserName }, Comment = x.Comment, Rate = x.Rate, ServiceOfferId = x.ServiceOfferId }).ToArray();
            ViewDetails model = new ViewDetails { Id = found.Id, Title = found.Title, Author = found.AuthorId, Description = found.Description, RatePerHour = found.RatePerHour, Dates = foundDates, Category = found.Category, Subcategory= found.Subcategory, Reviews = reviewstomodel };
            return View( model);
        }

        [HttpPost]
        public async Task<ActionResult> ViewDetails(ViewDetails edited)
        {
            var isUpdated = await _offerProxy.UpdateServiceOfferAsync
                (Mapping.Mapping.Map_ViewDetails_To_Offer(edited));
                if (isUpdated)
                {
                    return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
                }

            return RedirectToAction("ViewDetails", "ServiceOffer", new { id = edited.Id });
        }

        public async Task<ActionResult> Delete(int idd)
        {
            var isDeleted = await _offerProxy.DeleteServiceOfferAsync(idd);
            if (isDeleted)
                return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
            return RedirectToAction("ViewDetails", "ServiceOffer", new { id = idd});



        }
        public async Task<ActionResult> AddReview(int serviceId, string customer, string rate, string comment)
        {

            if (customer.Length <= 1)
            {
                TempData["msg"] = "<script>alert('You are not logged in.');</script>";
            }
            else
            {
                try
                {
                    var getAllBought  = await _offerProxy.GetAllBoughtAsync(customer);
                    var wasOrdered= getAllBought.Where(x => x.Id == serviceId).Count() > 0;
                    if (wasOrdered)
                    {
                        double rateD = Double.Parse(rate);
                        await _offerProxy.AddReviewAsync(new OfferReview { Comment = comment, CustomerId = customer, Rate = rateD, ServiceOfferId = serviceId });
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('You haven't bought that service.');</script>";
                    }
                }
                catch
                {
                    return View("Error", null);
                }
            }
           return RedirectToAction("ViewDetails","ServiceOffer", new { id=serviceId });
        }
    }
}