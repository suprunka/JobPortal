using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    [DataContract]
    public class User
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

        public User(string phoneNumber, string firstName, string lastName, string email, string userName, string password,
            string addressLine, string cityName, string postCode, Region region, Gender gender)
        {
        
            _phoneNumber = PhoneNumber;
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
        public User()
        {

        }

        [DataMember(IsRequired = true)]
        
        public virtual String PhoneNumber { get; set; }

        [DataMember(IsRequired = true)]
        public String FirstName { get; set; }

        [DataMember(IsRequired = true)]
        public String LastName { get; set; }

        [DataMember(IsRequired = true)]
        public String Email { get; set; }

        [DataMember(IsRequired = true)]
        public String UserName { get; set; }

        [DataMember(IsRequired = true)]
        public String Password { get; set; }

        [DataMember(IsRequired = true)]
        public String AddressLine { get; set; }

        [DataMember(IsRequired = true)]
        public String CityName { get; set; }

        [DataMember(IsRequired = true)]
        public String Postcode { get; set; }

        [DataMember(IsRequired = true)]
        public Region Region { get; set; }

        [DataMember(IsRequired = true)]
        public Gender Gender { get; set; }
    }
}
