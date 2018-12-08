using AppJobPortal.New;
using AppJobPortal.TcpOfferReference;
using AppJobPortal.TcpOrderReference;
using AppJobPortal.TcpUserReference;
using AutoMapper;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppJobPortal
{
    /// <summary>
    /// Interaction logic for Top10Services.xaml
    /// </summary>
    public partial class Top10Services : UserControl
    {
        private readonly IOfferService _proxyOffer;
        private readonly IUserService _proxyUser;
        private IMapper _mapper;
        private ServiceAppModel _serviceOffer;
        private IOrderService _orderproxy;

        public Top10Services()
        {
            InitializeComponent();
            _proxyOffer = new TcpOfferReference.OfferServiceClient("OfferServiceTcpEndpoint");
            _proxyUser = new TcpUserReference.UserServiceClient("UserServiceTcpEndpoint");
            _orderproxy = new OrderServiceClient("OrderServiceTcpEndpoint");
            _serviceOffer = new ServiceAppModel();
            GetAll();
            Init();


        }
        public void Init()
        {
            servicesTable.CanUserAddRows = false;
        }
        private void GetAll()
        {
            IList<ServiceAppModel> offersToAddToTable = new List<ServiceAppModel>();
           Dictionary<Offer, double> sortedMatching = new Dictionary<Offer, double>();

            var salelines = _orderproxy.GetAllSalelines();
            var offers = _proxyOffer.GetAllOffers();
            foreach (var offer in offers)
            {
                var orders= salelines.Where(x => x.ServiceOfferId == offer.Id).Count();
                var rating =_proxyOffer.GetAvgOfServiceRates(offer.Id);
                
                Double passed = orders * rating/ 11;
                sortedMatching.Add(offer, passed);
            }
            
           
            foreach (KeyValuePair<Offer, double> offer in sortedMatching.OrderBy(entry => entry.Value).Take(10))
            {
                var u = _proxyUser.FindUser(offer.Key.AuthorId);


                offersToAddToTable.Add(new ServiceAppModel()
                {
                    Id = offer.Key.Id,
                    Author_phone = u.PhoneNumber,
                    Category = offer.Key.Category,
                    Description = offer.Key.Description,
                    RatePerHour = offer.Key.RatePerHour,
                    Subcategory = offer.Key.Subcategory,
                    Title = offer.Key.Title,
                    FullName = u.FirstName + " " + u.LastName
                });

            }
            servicesTable.ItemsSource = offersToAddToTable;
        }

    }
}
