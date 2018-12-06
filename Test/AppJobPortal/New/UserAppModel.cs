using AppJobPortal.New;
using JobPortal.Model;
using System;


namespace AppJobPortal.Models
{
    public class UserAppModel : NotifyBase//, IDataErrorInfo
    {
        string phoneNumber, firstName, lastName, email, userName, password,
             addressLine, cityName, postCode, paypalmail, description;
        int Id;
        Region region;
        Gender gender;

        

        public UserAppModel()
        {
        }

        public UserAppModel(int iD, string paypalmail, string phoneNumber, string firstName, string lastName, string email,string username, string addressLine, string cityName, string postcode, Region region, Gender gender)
        {
            ID = iD;
            this.phoneNumber = phoneNumber;
            this.paypalmail = paypalmail;
           
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
       /* #region implementing IDataErrorInfo
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName))
                        result = "Please enter a First Name";
                }
                if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName))
                        result = "Please enter a Last Name";
                }
                if (columnName == "Email")
                {
                    if (!Email.Contains("@"))
                        result = "Please enter a valid email";
                }
                return result;
            }
        }
        #endregion*/
    }
    public class UserListModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName  { get; set; }
            
    }
}
