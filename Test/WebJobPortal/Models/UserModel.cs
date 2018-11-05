using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ServiceLibrary.Models;

namespace WebJobPortal.Models
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

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[0-9]{8}$")]
        public virtual String PhoneNumber { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String FirstName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String LastName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public virtual String Email { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{3,}$")]
        public virtual String UserName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,}$")]
        public virtual String Password { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String AddressLine { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String CityName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[0-9]{4}$")]
        public virtual String Postcode { get; set; }

        [EnumDataType(typeof(Region), ErrorMessage = "Phone number required")]
        public virtual Region Region { get; set; }

        [EnumDataType(typeof(Gender), ErrorMessage = "Phone number required")]
        public virtual Gender Gender { get; set; }
    }
}