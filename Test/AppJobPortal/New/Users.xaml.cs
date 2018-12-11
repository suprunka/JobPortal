using AppJobPortal.Models;
using AppJobPortal.TcpUserReference;
using AutoMapper;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AppJobPortal.New
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        private readonly IUserService _proxy;
        private IMapper _mapper;
        private UserAppModel _user;
        private User[] _source;


        public Users()
        {
            InitializeComponent();
            _proxy = new UserServiceClient("UserServiceTcpEndpoint");

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserAppModel, User>();
            });

            _mapper = config.CreateMapper();

            _user = new UserAppModel();
            Init();
            GetAll();
           

        }
        public Users(UserAppModel user)
        {
            InitializeComponent();
            _proxy = new UserServiceClient("UserServiceTcpEndpoint");

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserAppModel, User>();
            });

            _mapper = config.CreateMapper();

            _user = new UserAppModel();
            Init();
            GetAll();
            _user = user;
            SetTextBoxes();
        }

        public void Init()
        {
            regBox.ItemsSource = Enum.GetValues(typeof(Region));
        }
        private void GetAll() {
            _source = new UserServiceClient("UserServiceTcpEndpoint").GetAll();
            //_source.AsParallel().ForAll(user => new UserAppModel(user.ID, user.PhoneNumber, user.FirstName, user.LastName, user.Email, user.AddressLine, user.CityName, user.Postcode, user.Region, user.UsersGender));
            IList<UserAppModel> userAppModels = new List<UserAppModel>();
            foreach(User user in _source)
            {
                userAppModels.Add(new UserAppModel(user.ID, user.PayPalMail, user.PhoneNumber, user.FirstName, user.LastName, user.Email,user.UserName, user.AddressLine, user.CityName, user.Postcode, user.Region, user.Gender, user.LastUpdate));
             
            }
            usersTable.ItemsSource = userAppModels;
           


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            try
            {
                id = int.Parse(searchMe.Text);
            }
            catch
            {
                MessageBox.Show("Enter positive number", "Wrong input in search box");
            }
            try
            {
                User u = _proxy.FindUserByID(id);
                if (u != null)
                {
                    _user = _mapper.Map(u, new UserAppModel());
                    SetTextBoxes();
                    usersTable.SelectedItem = _user;
                }
                else
                {
                    MessageBox.Show("I can't find user with ID: " + id, "Cannot find user");
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("I can't find user with ID: " + id, "Cannot find user");
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (usersTable.SelectedIndex > -1)
            {
                SetUserFromBoxes();
                _user.ID = int.Parse(txtId.Text);

                if (!_proxy.EditUser(_mapper.Map(_user, new User())))
                {
                    MessageBox.Show("Data you read was modified", "Cannot process the operation");
                }
            }
            else
            {
                MessageBox.Show("Select user first" , "Cannot find user");

            }
            GetAll();
        }

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
          
           

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtFname.Text = "";
            txtId.Text = "";
            txtLname.Text = "";
            txtPhonenumber.Text = "";
            txtPostcode.Text = "";
            txtUsername.Text = "";
            txtPaypalMail.Text = "";
            lastUpdate.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            UserAppModel user = (UserAppModel)usersTable.SelectedItem;
            if (MessageBox.Show("Are you sure that you want to delete the user?",
                        "Confirmation", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var result = _proxy.DeleteUser(user.ID);
                if (result)
                {
                    GetAll();
                }
                else
                {
                    MessageBox.Show("I can't delete the user.");
                }

            }
            else
            {
                MessageBox.Show("I can't user with ID: " + _user.ID, "Cannot find user");
            }
        }


        private void usersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _user = _mapper.Map(usersTable.SelectedItem, new UserAppModel());
                SetTextBoxes();
            }
            catch
            {

            }
        }
        private void SetTextBoxes()
        {
            if (_user != null)
            {
                txtAddress.Text = _user.AddressLine;
                txtCity.Text = _user.CityName;
                txtEmail.Text = _user.Email;
                txtFname.Text = _user.FirstName;
                txtId.Text = _user.ID.ToString();
                txtLname.Text = _user.LastName;
                txtPhonenumber.Text = _user.PhoneNumber;
                txtPostcode.Text = _user.Postcode;
                txtUsername.Text = _user.UserName;
                regBox.SelectedValue = _user.Region;
                txtPaypalMail.Text = _user.PayPalMail;
                lastUpdate.DataContext = _user.LastUpdate;
                if (_user.Gender.ToString() == "Male")
                {
                    Male.IsChecked = true;
                }
                else
                {
                    Female.IsChecked = true;
                }
                
            }
        }
        private void SetUserFromBoxes()
        {
            try
            {
                _user.Region = (Region)regBox.SelectedItem;
                _user.AddressLine = txtAddress.Text;
                _user.CityName = txtCity.Text;
                _user.Email = txtEmail.Text;
                _user.FirstName = txtFname.Text;
                _user.LastName = txtLname.Text;
                _user.PhoneNumber = txtPhonenumber.Text;
                _user.Postcode = txtPostcode.Text;
                _user.UserName = txtUsername.Text;
                _user.PayPalMail = txtPaypalMail.Text;
                _user.LastUpdate =(System.Data.Linq.Binary) lastUpdate.DataContext;

                if ((bool)Male.IsChecked)
                {
                    _user.Gender = Gender.Male;
                }
                else if ((bool)Female.IsChecked)
                {
                    _user.Gender = Gender.Female;

                }
                else
                {
                }
            }
            catch
            {
                throw new FormatException();
            }
        }

        private void usersTable_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                _user = _mapper.Map(usersTable.SelectedItem, new UserAppModel());
                SetTextBoxes();
            }
            catch
            {

            }
        }

       
    }
}
