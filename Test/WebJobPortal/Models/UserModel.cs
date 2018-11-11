using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using JobPortal.Model;

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

        public virtual int ID { get; set; }

        [Display(Name = "Phone Number:")]
        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[0-9]{8}$")]
        public virtual String PhoneNumber { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First name required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$")]
        public virtual String FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last name required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "Last name characters must be included in danish alphabeth.")]  
        public virtual String LastName { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public virtual String Email { get; set; }

        [Display(Name = "User Name:")]
        [Required(ErrorMessage = "User name required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{3,}$")]
        public virtual String UserName { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password required")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,}$")]
        public virtual String Password { get; set; }

        [Display(Name = "Address:")]
        [Required(ErrorMessage = "Address (street and number) required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$")]
        public virtual String AddressLine { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "City required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$")]
        public virtual String CityName { get; set; }

        [Display(Name = "Postcode:")]
        [Required(ErrorMessage = "Postcode required")]
        [RegularExpression("^[0-9]{4}$")]
        public virtual String Postcode { get; set; }

        [Display(Name = "Region:")]
        [EnumDataType(typeof(Region), ErrorMessage = "Choose region")]
        public virtual Region Region { get; set; }

        [Display(Name = "Gender:")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Choose gender")]
        public virtual Gender Gender { get; set; }
    }
}