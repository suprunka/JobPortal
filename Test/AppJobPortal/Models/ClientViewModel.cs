using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppJobPortal.UserServiceReferenceTcp;
using AutoMapper;
using JobPortal.Model;
using Region = AppJobPortal.UserServiceReferenceTcp.Region;

namespace AppJobPortal.Models
{
    class UserViewModel :  ObservableCollection<UserAppModel>, INotifyPropertyChanged
    {
        private int selectedIndex ;
        private string selectedValue;
        private int id;
        private string phonenumber;
        private string email;
        private string firstname;
        private string lastName;
        private string username;
        private string password;
        private string address;
        private string city;
        private string postcode;
        private UserServiceReferenceTcp.Region region;
        private UserServiceReferenceTcp.Gender gender;
        private IMapper _mapper;
        private ICommand addUserCommand;
        private ICommand delUserCommand;
        private ICommand updateUserCommand;

        //private DateTime? birthdate;

        private readonly IUserService _proxy;
        public event PropertyChangedEventHandler PropertyChanged;


        public UserViewModel()
        {
            _proxy = new UserServiceReferenceTcp.UserServiceClient("UserServiceTcpEndpoint");
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserAppModel, UserServiceReferenceTcp.User>();

        });

        _mapper = config.CreateMapper();

            updateUserCommand = new CommandHandler(new Action(UpdateUser),()=> true);
            addUserCommand = new CommandHandler(new Action(AddUser),()=> true);
            delUserCommand = new CommandHandler(new Action(DeleteUser), () => true);

            //ViewUser();
            //if (_proxy.GetAll() != null || _proxy.GetAll.)
        }

      

        public int SelectedIndexOfCollection
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                this.OnPropertyChanged("SelectedIndex");
                OnPropertyChanged("Phonenumber");
                OnPropertyChanged("Firstname");
                OnPropertyChanged("Lastname");
                OnPropertyChanged("Email");
                OnPropertyChanged("Username");
                OnPropertyChanged("Password");
                OnPropertyChanged("Addressline");
                OnPropertyChanged("City");
                OnPropertyChanged("Postcode");
                OnPropertyChanged("Gender");
                OnPropertyChanged("Region");
            }
        }
       
        private ObservableCollection<Region> values = new ObservableCollection<Region>(Enum.GetValues(typeof(Region)).Cast<Region>());
        public ObservableCollection<Region> Values
        {
            get { return values; }
            set
            {
                values = value;
                OnPropertyChanged("Region");
            }
        }

        public string SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                OnPropertyChanged("Region");
            }
        }

        public int Id
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    
                    return this.Items[this.SelectedIndexOfCollection].ID;
                }
                else
                {
                    return id;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].ID = value;
                }
                else
                {
                    id = value;
                }
                OnPropertyChanged("Id");
            }
        }
        public string Phonenumber
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {

                    return this.Items[this.SelectedIndexOfCollection].PhoneNumber;
                }
                else
                {
                    return phonenumber;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].PhoneNumber = value;
                }
                else
                {
                    phonenumber = value;
                }
                OnPropertyChanged("Phonenumber");
            }
        }
        public string Email
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Email;
                }
                else
                {
                    return email;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Email = value;
                }
                else
                {
                    email = value;
                }
                OnPropertyChanged("Email");
            }
        }
        public string FirstName
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].FirstName;
                }
                else
                {
                    return firstname;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].FirstName = value;
                }
                else
                {
                    firstname = value;
                }
                OnPropertyChanged("Name");
            }
        }

        public string LastName
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].LastName;
                }
                else
                {
                    return lastName;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].LastName = value;
                }
                else
                {
                    lastName = value;
                }
                OnPropertyChanged("LastName");
            }
        }
        public string UserName
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].UserName;
                }
                else
                {
                    return username;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].UserName = value;
                }
                else
                {
                    username = value;
                }
                OnPropertyChanged("Username");
            }
        }
        public string Address
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].AddressLine;
                }
                else
                {
                    return address;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].AddressLine = value;
                }
                else
                {
                    address = value;
                }
                OnPropertyChanged("Addressline");
            }
        }
        public string City
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].CityName;
                }
                else
                {
                    return city;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].CityName = value;
                }
                else
                {
                    city = value;
                }
                OnPropertyChanged("City");
            }
        }
        public string Postcode
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Postcode;
                }
                else
                {
                    return postcode;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Postcode = value;
                }
                else
                {
                    postcode = value;
                }
                OnPropertyChanged("Postcode");
            }
        }
        public UserServiceReferenceTcp.Region Region
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Region;
                }
                else
                {
                    return region;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Region = value;

                }
                else
                {
                    region = value;
                }
                OnPropertyChanged("Region");
            }
        } public UserServiceReferenceTcp.Gender Gender
        {
            get
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Gender;
                }
                else
                {
                    return gender;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection >= 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Gender = value;

                }
                else
                {
                    gender = value;
                }
                OnPropertyChanged("Gender");
            }
        }



        private void ViewUser()
        {
            this.Clear();
            foreach (UserServiceReferenceTcp.User u in _proxy.GetAll())
            {
                UserAppModel userAppModel = _mapper.Map(u, new UserAppModel());
                this.Add(userAppModel);
            }
            

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
  
        private void AddUser()
        {
            if (SelectedIndexOfCollection >= 0)
            {
                UserAppModel userAppModel = new UserAppModel();
                userAppModel.FirstName = firstname;
                userAppModel.LastName = lastName;
                userAppModel.Password = password;
                userAppModel.PhoneNumber = phonenumber;
                userAppModel.Postcode = postcode;
                userAppModel.Region = region;
                userAppModel.UserName = username;
                userAppModel.CityName = city;
                userAppModel.AddressLine = address;
                userAppModel.Gender = gender;
                userAppModel.Email = email;
                if (_proxy.CreateUser(_mapper.Map(userAppModel, new UserServiceReferenceTcp.User())))
                {
                    ViewUser();
                }
            }
        }

        private void UpdateUser()
        {
            if (SelectedIndexOfCollection >= 0)
            {
                UserAppModel userAppModel = new UserAppModel();
                userAppModel.FirstName = firstname;
                userAppModel.LastName = lastName;
                userAppModel.Password = password;
                userAppModel.PhoneNumber = phonenumber;
                userAppModel.Postcode = postcode;
                userAppModel.Region = region;
                userAppModel.UserName = username;
                userAppModel.Password = password;
                userAppModel.CityName = city;
                userAppModel.AddressLine = address;
                userAppModel.Gender = gender;
                userAppModel.Email = email;
                userAppModel.ID = Id;
                _proxy.EditUser(_mapper.Map(userAppModel, new UserServiceReferenceTcp.User()));
            }
            else
            {
                MessageBox.Show("Select the user", "User isn't selected");

            }

        }

        private void DeleteUser()
        {
            
            if (selectedIndex < 0)
            {
                MessageBox.Show("Select the user", "User isn't selected");
            }
            else
            {
                if (MessageBox.Show("Are you sure that you want to delete the user?",
                        "Confirmation", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _proxy.DeleteUser(Id);
                }
            }
            ViewUser();
        }
        #region ICommand
        public ICommand AddUserCommand
        {
            get
            {
                return addUserCommand;
            }
            set
            {
                addUserCommand = value;
            }
        }

      
        public ICommand DelUserCommand
        {
            get
            {
                return delUserCommand;
            }
            set
            {
                delUserCommand = value;
            }
        }

        public ICommand UpdateUserCommand
        {
            get
            {
                return updateUserCommand;
            }
            set
            {
                updateUserCommand = value;
            }
        }
        #endregion




    }
}

