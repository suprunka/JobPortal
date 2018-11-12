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
using AppJobPortal.Model;
using AppJobPortal.Models;
using AppJobPortal.OfferServiceReference;
using AutoMapper;

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
            GetAll();
            Init();


        }
        public void Init()
        {
            servicesTable.CanUserAddRows = false;
        }
        private void GetAll()
        {
            _source = _proxy.GetAllOffers();
            ObservableCollection<ServiceAppModel> offers = new ObservableCollection<ServiceAppModel>();
            foreach (Offer offer in _source)
            {
                offers.Add(new ServiceAppModel() {Id = offer.Id, Author_phone = offer.Author.PhoneNumber,
                                                Category = offer.Category, Description = offer.Description,
                                                RatePerHour = offer.RatePerHour, Subcategory = offer.Subcategory,
                                                Title = offer.Title, FullName = offer.Author.FirstName +" "+ offer.Author.LastName});

            }
            servicesTable.ItemsSource = offers;
        }
       

        void Delete(object sender, RoutedEventArgs e)
        {
            ServiceAppModel offer = (ServiceAppModel)servicesTable.SelectedItem;
            if (_proxy.DeleteServiceOffer(offer.Id))
            {
                GetAll();
            }
            else
            {
                MessageBox.Show("Can't find service", "Can't find service");

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                var offer = _proxy.FindServiceOffer(id);
                //UserAppModel user = _mapper.Map(offer.Author, new UserAppModel());
                IList<ServiceAppModel> list = new List<ServiceAppModel>();
                list.Add(new ServiceAppModel
                {
                    Id = offer.Id,
                    Author = offer.Author,
                    Author_phone = offer.Author.PhoneNumber,
                    Category = offer.Category,
                    Description = offer.Description,
                    RatePerHour = offer.RatePerHour,
                    Subcategory = offer.Subcategory,
                    Title = offer.Title,
                    FullName = offer.Author.FirstName + " " + offer.Author.LastName
                });
                servicesTable.ItemsSource = list;


            }
            catch (FormatException)
            {
                MessageBox.Show("ID needs to be positive ", "Can't find ID");

            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Enter user's Id first", "Empty field");

            }
        }
        private void GoToEmployee(object sender, RoutedEventArgs e)
        {
            ServiceAppModel offer = (ServiceAppModel)servicesTable.SelectedItem;
           
            new Window1(_mapper.Map(offer.Author, new UserAppModel()));
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
    }
}
