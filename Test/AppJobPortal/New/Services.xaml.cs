using System.Collections.Generic;
using System.Windows.Controls;
using AppJobPortal.OfferServiceReference;
using AutoMapper;
using JobPortal.Model;

namespace AppJobPortal.New
{
    /// <summary>
    /// Interaction logic for Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        private readonly IOfferService _proxy;
        private IMapper _mapper;
        private ServiceAppModel _serviceOffer;
        private Offer[] _source;

        public Services()
        {
            InitializeComponent();
            _proxy = new OfferServiceClient("offerService");

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ServiceAppModel,Offer>();
            });

            _mapper = config.CreateMapper();

            _serviceOffer = new ServiceAppModel();
            Init();
            GetAll();

        }
        public void Init()
        {

        }
        private void GetAll()
        {
            _source = _proxy.GetAllOffers();
            //_source.AsParallel().ForAll(user => new UserAppModel(user.ID, user.PhoneNumber, user.FirstName, user.LastName, user.Email, user.AddressLine, user.CityName, user.Postcode, user.Region, user.Gender));
            IList<ServiceAppModel> offers = new List<ServiceAppModel>();
            foreach (Offer offer in _source)
            {
                offers.Add(new ServiceAppModel());

            }
            servicesTable.ItemsSource = offers;


        }

    }
}
