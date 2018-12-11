using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyWeb.OfferReference;
using PagedList;
using System.Data.Linq;

namespace WebJobPortal.Models
{

    public class ChangeDescriptionViewModel
    {
        public int ID { get; set; }

        public String Description { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email:")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class UserProfileViewModel
    {
        public virtual IPagedList<ManageOfferModel> Services { get; set; }

        public virtual BoughtOfferModel[] Bought { get; set; }
        public virtual JobOfferViewModel[] Jobs { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual String Description { get; set; }

        [Display(Name = "Email:")]
        public virtual String Email { get; set; }


        [Display(Name = "User name:")]
        public virtual String UserName { get; set; }

        [Display(Name = "Phone number:")]
        public virtual String PhoneNumber { get; set; }

        public virtual String ID { get; set; }

        [Display(Name = "First Name:")]
        public virtual String FirstName { get; set; }

        [Display(Name = "Last Name:")]
        public virtual String LastName { get; set; }

        [Display(Name = "Address:")]
        public virtual String AddressLine { get; set; }

        [Display(Name = "City:")]
        public virtual String CityName { get; set; }

        [Display(Name = "PayPal Mail:")]
        public virtual String PayPalMail { get; set; }

        [Display(Name = "Postcode:")]
        public virtual String Postcode { get; set; }

        [Display(Name = "Region:")]
        public virtual Region Region { get; set; }

        [Display(Name = "Gender:")]
        public virtual Gender Gender { get; set; }
        public virtual Binary Lastupdate { get; set; }
    }

    public class CustomerViewModel
    {
        public String PhoneNumber { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String AddressLine { get; set; }

        public String CityName { get; set; }

        public String PayPalMail { get; set; }

        public String Postcode { get; set; }

        public Region Region { get; set; }

        public Gender Gender { get; set; }
        public string Email { get; set; }


    }

    public class SetPropertiesViewModel
    {

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First name required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "At least one character required.")]
        public virtual String FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last name required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "Last name characters must be included in danish alphabeth.")]
        public virtual String LastName { get; set; }

        [Display(Name = "Address:")]
        [Required(ErrorMessage = "Address (street and number) required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "At least one character required.")]
        public virtual string AddressLine { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "City required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "At least one character required.")]
        public virtual String CityName { get; set; }

        [Display(Name = "PayPal mail:")]
        [Required(ErrorMessage = "PayPal required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Must include danish letters, dot and at signs.")]
        public virtual String PayPalMail { get; set; }

        [Display(Name = "Postcode:")]
        [Required(ErrorMessage = "Postcode required")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Four digits required.")]
        public virtual String Postcode { get; set; }

        [Display(Name = "Region:")]
        [EnumDataType(typeof(Region), ErrorMessage = "Choose region")]
        public virtual Region Region { get; set; }

        [Display(Name = "Gender:")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Choose gender")]
        public virtual Gender Gender { get; set; }
    }

    public class ReviewAuthorViewModel
    {
        public Gender Gender { get; set; }
        public string Username { get; set; }
    }

    public class RegisterViewModel
    {
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Phone Number:")]
        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "8 digits required.")]
        public String PhoneNumber { get; set; }


        [Display(Name = "User Name:")]
        [Required(ErrorMessage = "User name required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{3,}$", ErrorMessage = "At leat 3 characters required.")]
        public String UserName { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeEmailViewModel
    {
        public UserProfileViewModel UserProfileViewModel { get; set; }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Current email")]
        public string OldEmail { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "New email")]
        public string NewEmail { get; set; }



    }

}