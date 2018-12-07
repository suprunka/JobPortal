using AppJobPortal.New;
using JobPortal.Model;
using System;


namespace AppJobPortal.Models
{
    public class UserAppModelDisplay 
    {
        string phoneNumber, firstName, lastName, email, userName, password,
             addressLine, cityName, postCode, paypalmail, description;
        int Id;
        Region region;
        UsersGender gender;



        public UserAppModelDisplay()
        {
        }

        public UserAppModelDisplay(int iD, string paypalmail, string description, string phoneNumber, string firstName, string lastName, string email, string username, string addressLine, string cityName, string postcode, Region region, UsersGender gender)
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
                phoneNumber = value;
            }
        }

        public virtual String FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }

        public virtual String LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
            }
        }

        public virtual String Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        public virtual String UserName
        {
            get { return userName; }
            set
            {
                userName = value;
            }
        }

        public virtual String AddressLine
        {
            get { return addressLine; }
            set
            {
                addressLine = value;
            }
        }

        public virtual String CityName
        {
            get { return cityName; }
            set
            {
                cityName = value;
            }
        }

        public virtual String PayPalMail
        {
            get { return paypalmail; }
            set
            {
                paypalmail = value;
            }
        }

        public virtual String Description
        {
            get { return description; }
            set
            {
                description = value;
            }
        }

        public virtual String Postcode
        {
            get { return postCode; }
            set
            {
                postCode = value;
            }
        }

        public virtual Region Region
        {
            get { return region; }
            set
            {
                region = value;
            }
        }

        public virtual UsersGender Gender
        {
            get { return gender; }
            set
            {
                gender = value;
            }
        }

    }
}
