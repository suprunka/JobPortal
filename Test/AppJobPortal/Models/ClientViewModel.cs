using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppJobPortal.UserServiceReferenceTcp;
using AutoMapper;
using JobPortal.Model;

namespace AppJobPortal.Models
{
    class ClientViewModel :  ObservableCollection<UserAppModel>, INotifyPropertyChanged
    {
        private int selectedIndex = -2;

        private int id;
        private string firstname;
        private string lastName;
        private string username;
        private string password;
        private string address;
        private string city;
        private string postcode;
        private string region;
        private string gender;
        //private DateTime? birthdate;

        private readonly IUserService _proxy;
        public event PropertyChangedEventHandler PropertyChanged;


        public ClientViewModel()
        {
            _proxy = new UserServiceReferenceTcp.UserServiceClient("UserServiceTcpEndpoint");
           
            /*updateClientCommand = new CommandBase(param => this.UpdateClient());
            AddClientCommand = new CommandBase(param => this.AddClient());
            NewClientCommand = new CommandBase(new Action<Object>(NewClient));
            DelClientCommand = new CommandBase(param => this.DelClient());*/
            ViewClient();

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
                this.OnPropertyChanged("Selected Index");
                OnPropertyChanged("Phone number");

                OnPropertyChanged("First name");
                OnPropertyChanged("Last name");
                OnPropertyChanged("Email");
                OnPropertyChanged("User name");
                OnPropertyChanged("Password");
                OnPropertyChanged("Address line");
                OnPropertyChanged("City");
                OnPropertyChanged("Post code");
                OnPropertyChanged("Gender");
                OnPropertyChanged("Region");
            }
        }


        private ObservableCollection<string> values = new ObservableCollection<string>()//Combobox region
        {
            "ffff","dhg"
        };
        public ObservableCollection<string> Values
        {
            get { return values; }
            set
            {
                values = value;
                OnPropertyChanged("Region");
            }
        }
        private string selectedValue;
        private IMapper _mapper;

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
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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

        public string FirstName
        {
            get
            {
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
                {
                    this.Items[this.SelectedIndexOfCollection].UserName = value;
                }
                else
                {
                    username = value;
                }
                OnPropertyChanged("User name");
            }
        }
        public string Address
        {
            get
            {
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
                {
                    this.Items[this.SelectedIndexOfCollection].AddressLine = value;
                }
                else
                {
                    address = value;
                }
                OnPropertyChanged("Address");
            }
        }
        public string City
        {
            get
            {
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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
                if (this.SelectedIndexOfCollection > -1)
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



        private void ViewClient()
        {

            foreach (var u in _proxy.GetAll())
            {
                UserAppModel userAppModel= Mapper.Map(u, new UserAppModel());
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
        
        
    }
}

