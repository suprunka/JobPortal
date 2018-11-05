using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;//
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

        [Required(ErrorMessage = "Phone number is required")]
        [DisplayName("Phone Number:")]
        [RegularExpression("^[0-9]{8}$")]
        public virtual String PhoneNumber { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [DisplayName("First Name:")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last Name:")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email:")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public virtual String Email { get; set; }

        [Required(ErrorMessage = "User name is required")]
        [DisplayName("User Name:")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{3,}$")]
        public virtual String UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password:")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,}$")]
        public virtual String Password { get; set; }

        [Required(ErrorMessage = "Address (streeet and number) is required")]
        [DisplayName("Address:")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String AddressLine { get; set; }

        [Required(ErrorMessage = "City is required")]
        [DisplayName("City:")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String CityName { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [DisplayName("Postcode:")]
        [RegularExpression("^[0-9]{4}$")]
        public virtual String Postcode { get; set; }

        [EnumDataType(typeof(Region), ErrorMessage = "Choose region")]
        [DisplayName("Region:")]
        public virtual Region Region { get; set; }

        [EnumDataType(typeof(Region), ErrorMessage ="Choose gender")]
        [DisplayName("Gender:")]
        public virtual Gender Gender { get; set; }
    }
}
        