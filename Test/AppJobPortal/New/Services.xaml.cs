using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using AppJobPortal.Models;
using AppJobPortal.TcpUserReference;
using AutoMapper;
using AppJobPortal.TcpOfferReference;
using JobPortal.Model;
using System.Windows.Controls;

namespace AppJobPortal.New
{

    public partial class Services : UserControl
    {
        private readonly IOfferService _proxyOffer;
        private readonly IUserService _proxyUser;
        private IMapper _mapper;
        private ServiceAppModel _serviceOffer;
        private Offer[] _source;



        public Services()
        {
            InitializeComponent();
            _proxyOffer = new TcpOfferReference.OfferServiceClient("OfferServiceTcpEndpoint");
            _proxyUser = new TcpUserReference.UserServiceClient("UserServiceTcpEndpoint");
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
            _source = _proxyOffer.GetAllOffers();
            ObservableCollection<ServiceAppModel> offers = new ObservableCollection<ServiceAppModel>();
            foreach (Offer offer in _source)
            {
                var u = _proxyUser.FindUser(offer.AuthorId);


                     offers.Add(new ServiceAppModel() {Id = offer.Id, Author_phone = u.PhoneNumber,
                                                Category = offer.Category, Description = offer.Description,
                                                RatePerHour = offer.RatePerHour, Subcategory = offer.Subcategory,
                                                Title = offer.Title, FullName = u.FirstName +" "+ u.LastName});

            }
            servicesTable.ItemsSource = offers;
        }
       

        void Delete(object sender, RoutedEventArgs e)
        {
            ServiceAppModel offer = (ServiceAppModel)servicesTable.SelectedItem;
            if (_proxyOffer.DeleteServiceOffer(offer.Id))
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
                var offer = _proxyOffer.FindServiceOffer(id);
                var u = _proxyUser.FindUser(offer.AuthorId);
                //UserAppModel user = _mapper.Map(offer.Author, new UserAppModel());
                IList<ServiceAppModel> list = new List<ServiceAppModel>();
                list.Add(new ServiceAppModel
                {
                    Id = offer.Id,
                    Author_phone = u.PhoneNumber,
                    Category = offer.Category,
                    Description = offer.Description,
                    RatePerHour = offer.RatePerHour,
                    Subcategory = offer.Subcategory,
                    Title = offer.Title,
                    FullName = u.FirstName + " " + u.LastName
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
           
            new Users(_mapper.Map(offer.Author, new UserAppModel()));
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
    }
}
