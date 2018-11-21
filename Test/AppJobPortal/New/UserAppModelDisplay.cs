using AppJobPortal.New;
using AppJobPortal.UserServiceReferenceTcp;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gender = AppJobPortal.UserServiceReferenceTcp.Gender;
using Region = AppJobPortal.UserServiceReferenceTcp.Region;

namespace AppJobPortal.Models
{
    public class UserAppModelDisplay : NotifyBase//, IDataErrorInfo
    {
        string phoneNumber, firstName, lastName, email, userName, password,
             addressLine, cityName, postCode, paypalmail, description;
        int Id;
        Region region;
        Gender gender;



        public UserAppModelDisplay()
        {
        }

        public UserAppModelDisplay(int iD, string paypalmail, string description, string phoneNumber, string firstName, string lastName, string email, string username, string addressLine, string cityName, string postcode, Region region, Gender gender)
        {
            ID = iD;
            this.phoneNumber = phoneNumber;
            this.paypalmail = paypalmail;
            this.description = description;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.userName = username;
            this.addressLine = addressLine;
            this.cityName = cityName;
            this.postCode = postcode;
            this.region = region;
            this.gender = gender;
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
                OnPropertyChanged("Phonenumber");
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
                OnPropertyChanged("Lastname");
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
                OnPropertyChanged("Username");
                userName = value;
            }
        }

        public virtual String AddressLine
        {
            get { return addressLine; }
            set
            {
                OnPropertyChanged("Addressline");
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

        public virtual String PayPalMail
        {
            get { return paypalmail; }
            set
            {
                OnPropertyChanged("PayPalMail");
                paypalmail = value;
            }
        }

        public virtual String Description
        {
            get { return description; }
            set
            {
                OnPropertyChanged("Description");
                description = value;
            }
        }

        public virtual String Postcode
        {
            get { return postCode; }
            set
            {
                OnPropertyChanged("Postcode");
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
