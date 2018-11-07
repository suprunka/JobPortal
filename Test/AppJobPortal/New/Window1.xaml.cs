using AppJobPortal.Models;
using AppJobPortal.UserServiceReferenceTcp;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AppJobPortal.New
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly IUserService _proxy;
        private IMapper _mapper;
        private UserAppModel _user;
        private User[] _source;

        public Window1()
        {
            InitializeComponent();
            _proxy = new UserServiceReferenceTcp.UserServiceClient("UserServiceTcpEndpoint");

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserAppModel, UserServiceReferenceTcp.User>();
                 });

            _mapper = config.CreateMapper();

            _user = new UserAppModel();
            Init();
            GetAll();

        }
        public void Init()
        {
            regBox.ItemsSource = Enum.GetValues(typeof(Region)).Cast<Region>();
            
        }
        private void GetAll() {
            _source = _proxy.GetAll();
            //_source.AsParallel().ForAll(user => new UserAppModel(user.ID, user.PhoneNumber, user.FirstName, user.LastName, user.Email, user.AddressLine, user.CityName, user.Postcode, user.Region, user.Gender));
            IList<UserAppModel> userAppModels = new List<UserAppModel>();
            foreach(User user in _source)
            {
                userAppModels.Add(new UserAppModel(user.ID, user.PhoneNumber, user.FirstName, user.LastName, user.Email,user.UserName, user.AddressLine, user.CityName, user.Postcode, user.Region, user.Gender));
             
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
           User u =  _proxy.FindUser(id);
            if (u != null)
            {
                _user = _mapper.Map(u, new UserAppModel());
                SetTextBoxes();
                
            }
            else
            {
                MessageBox.Show("I can't find user with id"+ id, "Cannot find user");
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (usersTable.SelectedIndex > -1)
            {
                SetUserFromBoxes();
                _user.ID = int.Parse(txtId.Text);

                if (_proxy.EditUser(_mapper.Map(_user, new User())))
                {
                    GetAll();
                }
            }
            else
            {
                MessageBox.Show("Select user first" , "Cannot find user");

            }
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

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            UserAppModel user =(UserAppModel) usersTable.SelectedItem;
            if (MessageBox.Show("Are you sure that you want to delete the user?",
                        "Confirmation", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _proxy.DeleteUser(user.ID);
                GetAll();
            }
            else
            {
                MessageBox.Show("I can't uuuu user with id" + _user.ID, "Cannot find user");

            }

        }

        private void btnRgister_Click(object sender, RoutedEventArgs e)
        {
            SetUserFromBoxes();
            if (_proxy.CreateUser(_mapper.Map(_user, new User())))
            {
                GetAll();
            }
            else {
                MessageBox.Show("Wrong input", "Input failure");

            }

        }

        private void usersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _user = _mapper.Map(usersTable.SelectedItem, new UserAppModel());
            SetTextBoxes();

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
                
            }
        }
        private void SetUserFromBoxes()
        {

            _user.AddressLine = txtAddress.Text;
            _user.CityName = txtCity.Text;
            _user.Email = txtEmail.Text;
            _user.FirstName = txtFname.Text;
            _user.LastName = txtLname.Text;
            _user.PhoneNumber = txtPhonenumber.Text;
            _user.Postcode = txtPostcode.Text;
            _user.UserName = txtUsername.Text;
            _user.Password = txtPassword.Password;
        }

        private void usersTable_CurrentCellChanged(object sender, EventArgs e)
        {
            _user = _mapper.Map(usersTable.SelectedItem, new UserAppModel());
            SetTextBoxes();
        }

       

    }
}
