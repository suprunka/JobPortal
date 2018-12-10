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
using MyWeb.OrderReference;
using MyWeb.UserReference1;

namespace MyWeb.Controllers
{
    public class ServiceOfferController : Controller
    {

        private OfferReference.IOfferService _offerProxy;
        private UserReference1.IUserService _userProxy;
        private OrderReference.IOrderService _orderProxy;
        private IMapper _mapper;
        private IList<WorkingHoursOfOfferModel> workingdays = new List<WorkingHoursOfOfferModel>();


        // GET: Offer

        public ServiceOfferController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobPortal.Model.Offer, ManageOfferModel>();
            });

            _mapper = config.CreateMapper();
            _offerProxy = new OfferReference.OfferServiceClient("OfferServiceHttpEndpoint");
            _orderProxy = new OrderReference.OrderServiceClient("OrderServiceHttpEndpoint");
            _userProxy = new UserReference1.UserServiceClient("UserServiceHttpEndpoint1");


        }
        public ServiceOfferController(IOfferService proxy, IUserService userProxy, IOrderService orderProxy)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobPortal.Model.Offer, ManageOfferModel>();
            });
            _mapper = config.CreateMapper();
            _offerProxy = proxy;
            _userProxy = userProxy;
            _orderProxy = orderProxy;
        }

        public ServiceOfferController(IOfferService proxy)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobPortal.Model.Offer, ManageOfferModel>();
            });
            _mapper = config.CreateMapper();
            _offerProxy = proxy;
        }
        public async Task<ActionResult> Index(string searchingString, int? page, bool? showInRegion, int? sorting)
        {
            User profile = null;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var show = showInRegion.HasValue ? Convert.ToBoolean(showInRegion) : false;
            if (User.Identity.IsAuthenticated)
            {
                profile = _userProxy.FindUser(User.Identity.GetUserId());
            }
            var all = await _offerProxy.GetAllOffersAsync();
            IEnumerable<Offer> list = null;
            IPagedList<ManageOfferModel> ipagedList = null;
            switch (sorting)
            {
                case 1://by highet price
                    list = User.Identity.GetUserId() != null && show ?
                all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region).OrderByDescending(x => x.RatePerHour) :
                all.OrderByDescending(x => x.RatePerHour);
                    break;
                case 2:
                    list = User.Identity.GetUserId() != null && show ?
               all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region).OrderBy(x => x.RatePerHour) :

               all.OrderBy(x => x.RatePerHour);

                    break;
                case 4://date
                    list = User.Identity.GetUserId() != null && show  ?
                        all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region):

                    all.Select(x=>x);

                    break;
                case 3:
                    list = User.Identity.GetUserId() != null && show  ?
                        all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region ).OrderByDescending(x => x.Id):

                    all.OrderByDescending(x => x.Id);

                    break;
                default:
                    list = User.Identity.GetUserId() != null && show ?
                 all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region).OrderByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id)
                 as IComparable).ThenBy(x => x.RatePerHour) :

                 all.OrderByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id) as IComparable).ThenBy(x => x.RatePerHour);
                    break;
            }
            if (searchingString != null)
            {
                ipagedList = list.Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).Select(x => _mapper.Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12);
            }
            else
            {
                ipagedList = list.Select(x => _mapper.Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12);
            }
            // if (searchingString == null)
            // {
            //     return View("Index", list);
            // }
            //
            // var condition = User.Identity.GetUserId() != null && show ?
            //
            //     all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region && x.Title.ToUpper().Contains(searchingString.ToUpper())).
            //     OrderByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id) as IComparable).ThenBy(x => x.RatePerHour).
            //     Select(x => _mapper.Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12) :
            //
            //     all.Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper())).OrderBy(x => x.RatePerHour).
            //     ThenByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id) as IComparable).Select(x => _mapper.
            //     Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12);
            //
            // return View("Index", condition);
            return View("Index", ipagedList);
        }

        public async Task<ActionResult> Home(string searchingString, int? page, bool? showInRegion)
        {
            return await OpenSubcategory(searchingString, page, showInRegion, "Home");
        }

        public async Task<ActionResult> Tutoring(string searchingString, int? page, bool? showInRegion)
        {
            return await OpenSubcategory(searchingString, page, showInRegion, "Tutoring");
        }

        public async Task<ActionResult> IT(string searchingString, int? page, bool? showInRegion)
        {
            return await OpenSubcategory(searchingString, page, showInRegion, "IT");
        }

        public async Task<ActionResult> Repairs(string searchingString, int? page, bool? showInRegion)
        {
            return await OpenSubcategory(searchingString, page, showInRegion, "Repairs");
        }

        public async Task<ActionResult> Architecture(string searchingString, int? page, bool? showInRegion)
        {
            return await OpenSubcategory(searchingString, page, showInRegion, "Architecture");
        }

        public async Task<ActionResult> Media(string searchingString, int? page, bool? showInRegion)
        {
            return await OpenSubcategory(searchingString, page, showInRegion, "Media");
        }

        private async Task<ActionResult> OpenSubcategory(string searchingString, int? page, bool? showInRegion, string subCategoryName)
        {
            User profile = null;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var show = showInRegion.HasValue ? Convert.ToBoolean(showInRegion) : false;
            if (User.Identity.GetUserId() != null)
            {
                profile = _userProxy.FindUser(User.Identity.GetUserId());
            }
            var all = await _offerProxy.GetAllOffersAsync();

            var list = User.Identity.GetUserId() != null && show ?
                all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region && x.Category.ToString() == subCategoryName).
                OrderByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id) as IComparable).ThenBy(x => x.RatePerHour).Select(x =>
                _mapper.Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12) :

                all.Where(x => x.Category.ToString() == subCategoryName).OrderByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id)
                as IComparable).ThenBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12);

            if (searchingString == null)
            {
                return View("Index", list);
            }

            var condition = User.Identity.GetUserId() != null && show ?
                all.Where(x => _userProxy.FindUser(x.AuthorId).Region == profile.Region && x.Title.ToUpper().Contains(searchingString.
                ToUpper()) && x.Category.ToString() == subCategoryName).OrderByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id) as
                IComparable).ThenBy(x => x.RatePerHour).Select(x => _mapper.Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12) :

                all.Where(x => x.Title.ToUpper().Contains(searchingString.ToUpper()) && x.Category.ToString() == subCategoryName).
                OrderBy(x => x.RatePerHour).ThenByDescending(x => _offerProxy.GetAvgOfServiceRates(x.Id) as IComparable).
                Select(x => _mapper.Map(x, new ManageOfferModel())).ToPagedList(pageIndex, 12);
            return View("Index", condition);

        }

        public async Task<ActionResult> Add()
        {

            return View("Add", new AddOfferModel());


        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AddOfferModel model)
        {
            bool result = await _offerProxy.CreateServiceOfferAsync(
                Mapping.Mapping.Map_AddServiceOfferModel_To_Offer(model, User.Identity.GetUserId()));
            if (result)
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
        public async Task<ActionResult> GetHoursFrom(int serviceId, DateTime date)
        {
            try
            {
                var hours = await _orderProxy.GetHoursFromAsync(serviceId, date);
                var hoursFrom = hours.Select(x => new SelectListItem()
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
        public async Task<ActionResult> GetHoursTo(int serviceId, DateTime date, TimeSpan from)
        {

            var hours = await _orderProxy.GetHoursToAsync(serviceId, date, from);
            var hoursto = hours.Select(x => new SelectListItem()
            {
                Text = x.ToString(),
                Value = x.ToString(),
            })
                .ToList();

            return Json(hoursto, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public async Task<ActionResult> ViewDetails(int id)
        {
            ReviewModel[] reviewstomodel = null;
            var found = await _offerProxy.FindServiceOfferAsync(id);
            var foundDates = _offerProxy.GetAllWorkingDays().Where(x => x.OfferId == id);
            var reviews = _offerProxy.GetServiceReviews(id);
            if (reviews != null)
            {
                reviewstomodel = reviews.Select(x => new ReviewModel
                {
                    Customer = new ReviewAuthorViewModel
                    {
                        Gender =
                _userProxy.FindUser(x.CustomerId).Gender,
                        Username =
                _userProxy.FindUser(x.CustomerId).UserName
                    },
                    Comment =
                x.Comment,
                    Rate = x.Rate,
                    ServiceOfferId = x.ServiceOfferId
                }).ToArray();
            }

            ViewDetailsModel model = new ViewDetailsModel
            {
                Id = found.Id,
                Title = found.Title,
                Author = found.AuthorId,
                Description = found.Description,
                RatePerHour = found.RatePerHour,
                Dates = foundDates,
                Category = found.Category,
                Subcategory = found.Subcategory,
                Reviews = reviewstomodel
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ViewDetails(ViewDetailsModel edited)
        {
            var isUpdated = await _offerProxy.UpdateServiceOfferAsync
                (Mapping.Mapping.Map_ViewDetails_To_Offer(edited));
            if (isUpdated)
            {
                return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
            }

            return RedirectToAction("ViewDetailsModel", "ServiceOffer", new { id = edited.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await _offerProxy.DeleteServiceOfferAsync(id);
            if (isDeleted)
                return RedirectToAction("UserProfile", "User", new { id = User.Identity.GetUserId() });
            return RedirectToAction("ViewDetailsModel", "ServiceOffer", new { id = id });



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
                    var getAllBought = await _offerProxy.GetAllBoughtAsync(customer);
                    var wasOrdered = getAllBought.Where(x => x.Id == serviceId).Count() > 0;
                    if (wasOrdered)
                    {
                        double rateD = Double.Parse(rate);
                        await _offerProxy.AddReviewAsync(new OfferReview { Comment = comment, CustomerId = customer, Rate = rateD, ServiceOfferId = serviceId });
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('You haven\'t bought that service.');</script>";
                    }
                }
                catch
                {
                    return View("Error", null);
                }
            }
            return RedirectToAction("ViewDetailsModel", "ServiceOffer", new { id = serviceId });
        }
    }
}