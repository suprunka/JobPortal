using System.Web.Mvc;

namespace WebJobPortal.Controllers
{
    public class ServiceOfferController : Controller
    {
    }
}
/*
        private IOfferService _proxy;
        private IUserService userService;
        private IMapper _mapper;
        public ServiceOfferController(IOfferService proxy)
        {
            _proxy = proxy;
            userService = new UserServiceClient();
        }
        public ServiceOfferController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Offer, ServiceOfferWebModel>();
            });

            _mapper = config.CreateMapper();

            _proxy = new OfferReference.OfferServiceClient("offerService");
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

        public ActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public ActionResult Add(ServiceOfferWebModel serviceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Offer u = new Offer
                    {
                        Category = serviceModel.Category,
                        Description = serviceModel.Description,
                        RatePerHour = serviceModel.RatePerHour,
                        Subcategory = serviceModel.Subcategory,
                        Title = serviceModel.Title,
                        Author = userService.FindUser(serviceModel.AuthorNumber),
                    };
                    _proxy.CreateServiceOffer(u);
                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Creation done.";
                    return View("Create", new ServiceOfferWebModel());
                }
                else
                {
                    return View("Create", serviceModel);
                }
            }
            catch
            {
                return View("Create");
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var service = _proxy.FindServiceOffer((int)id);
            ServiceOfferWebModel s = new ServiceOfferWebModel
            {
                Category = service.Category,
                AuthorNumber = service.Author.PhoneNumber,
                Description = service.Description,
                Id = service.Id,
                RatePerHour = service.RatePerHour,
                Subcategory = service.Subcategory,
                Title = service.Title,
            };
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var service = _proxy.FindServiceOffer((int)id);
            ServiceOfferWebModel s = new ServiceOfferWebModel
            {
                Category = service.Category,
                AuthorNumber = service.Author.PhoneNumber,
                Description = service.Description,
                Id = service.Id,
                RatePerHour = service.RatePerHour,
                Subcategory = service.Subcategory,
                Title = service.Title,
            };
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceOfferWebModel serviceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Offer u = new Offer
                    {
                        Category = serviceModel.Category,
                        Description = serviceModel.Description,
                        RatePerHour = serviceModel.RatePerHour,
                        Subcategory = serviceModel.Subcategory,
                        Title = serviceModel.Title,
                        Author = userService.FindUser(serviceModel.AuthorNumber),
                    };
                    _proxy.UpdateServiceOffer(u);
                }
                return View("Add");
            }
            catch
            {
                return View("Add");
            }
        }


        [HttpGet]
        public ActionResult Delete(ServiceOfferWebModel serviceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Offer u = new Offer
                    {
                        Category = serviceModel.Category,
                        Description = serviceModel.Description,
                        RatePerHour = serviceModel.RatePerHour,
                        Subcategory = serviceModel.Subcategory,
                        Title = serviceModel.Title,
                        Author = userService.FindUser(serviceModel.AuthorNumber),
                    };
                    _proxy.UpdateServiceOffer(u);
                }
                return View("Create");
            }
            catch
            {
                return View("Create");
            }
        }
    }
}*/