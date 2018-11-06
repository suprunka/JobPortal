using AppJobPortal.UserServiceReferenceTcp;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gender = AppJobPortal.UserServiceReferenceTcp.Gender;
using Region = AppJobPortal.UserServiceReferenceTcp.Region;

namespace AppJobPortal.Models
{
    public class UserAppModel : NotifyBase
    {
        string phoneNumber, firstName, lastName, email, userName, password,
             addressLine, cityName, postCode;
        int Id;
        Region region;
        Gender gender;

        public UserAppModel(int Id,string phoneNumber, string firstName, string lastName, string email, string userName, string password,
            string addressLine, string cityName, string postCode, Region region, Gender gender)
        {
            this.Id = Id;
            this.phoneNumber = phoneNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.userName = userName;
            this.password = password;
            this.addressLine = addressLine;
            this.cityName = cityName;
            this.postCode = postCode;
            this.region = region;
            this.gender = gender;
        }

        public UserAppModel()
        {

        }

        public virtual int ID
        {
            get; set;
        }
        public virtual String PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                OnPropertyChanged("Phone number");
                phoneNumber = value;
            }
        }



        public virtual String FirstName
        {
            get { return firstName; }
            set
            {
                OnPropertyChanged("First name");
                firstName = value;
            }
        }

        public virtual String LastName
        {
            get { return lastName; }
            set
            {
                OnPropertyChanged("Last name");
                lastName = value;
            }
        }

        public virtual String Email
        {
            get { return email; }
            set
            {
                OnPropertyChanged("Email");
                email = value;
            }
        }

        public virtual String UserName
        {
            get { return userName; }
            set
            {
                OnPropertyChanged("User name");
                userName = value;
            }
        }

        public virtual String Password
        {
            get { return password; }
            set
            {
                OnPropertyChanged("Password");
                password = value;
            }
        }

        public virtual String AddressLine
        {
            get { return addressLine; }
            set
            {
                OnPropertyChanged("Address line");
                addressLine = value;
            }
        }

        public virtual String CityName
        {
            get { return cityName; }
            set
            {
                OnPropertyChanged("City");
                cityName = value;
            }
        }

        public virtual String Postcode
        {
            get { return postCode; }
            set
            {
                OnPropertyChanged("Post code");
                postCode = value;
            }
        }

        public virtual Region Region
        {
            get { return region; }
            set
            {
                OnPropertyChanged("Region");
                region = value;
            }
        }

        public virtual Gender Gender
        {
            get { return gender; }
            set
            {
                OnPropertyChanged("Gender");
                gender = value;
            }
        }
    }
}
