
using AppJobPortal.UserServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJobPortal.Models
{
    public class UserModel
    {
        private readonly string _phoneNumber;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _email;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _addressLine;
        private readonly string _cityName;
        private readonly string _postCode;
        private readonly Region _region;
        private readonly Gender _gender;

        public UserModel(string phoneNumber, string firstName, string lastName, string email, string userName, string password,
            string addressLine, string cityName, string postCode, Region region, Gender gender)
        {
            _phoneNumber = phoneNumber;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _userName = userName;
            _password = password;
            _addressLine = addressLine;
            _cityName = cityName;
            _postCode = postCode;
            _region = region;
            _gender = gender;
        }

        public UserModel()
        {

        }

        public virtual int ID { get; set; }

        public virtual String PhoneNumber { get; set; }

        public virtual String FirstName { get; set; }

        public virtual String LastName { get; set; }

        public virtual String Email { get; set; }

        public virtual String UserName { get; set; }

        public virtual String Password { get; set; }

        public virtual String AddressLine { get; set; }

        public virtual String CityName { get; set; }

        public virtual String Postcode { get; set; }

        public virtual Region Region { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
