using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyWeb.OfferReference;
using PagedList;

namespace WebJobPortal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class DescriptionViewModel
    {
        public int ID { get; set; }

        public String Description { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code:")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }
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


    public class JobOffer
    {
        public DateTime CurrentDate { get; set; }
        public CustomerViewModel Customer { get; set; }
        public decimal TotalPrice { get; set; }
        public BoughtOffers Offer { get; set; }

    }

    public class UserProfileViewModel
    {
        public IPagedList<ManageOffers> Services { get; set; }

        public BoughtOffers[] Bought { get; set; }
        public WebJobPortal.Models.JobOffer[] Jobs { get; set; }
        public DateTime Date { get; set; }

        public String Description { get; set; }

        [Display(Name = "Email:")]
        public String Email { get; set; }


        [Display(Name = "User name:")]
        public String UserName { get; set;}

        [Display(Name = "Phone number:")]
        public String PhoneNumber { get; set; }

        public String ID { get; set; }

        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name:")]
        public String LastName { get; set; }

        [Display(Name = "Address:")]
        public String AddressLine { get; set; }

        [Display(Name = "City:")]
        public String CityName { get; set; }

        [Display(Name = "PayPal Mail:")]
        public String PayPalMail { get; set; }

        [Display(Name = "Postcode:")]
        public String Postcode { get; set; }

        [Display(Name = "Region:")]
        public Region Region { get; set; }

        [Display(Name = "Gender:")]
        public Gender Gender { get; set; }
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
        public String FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last name required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "Last name characters must be included in danish alphabeth.")]
        public String LastName { get; set; }

        [Display(Name = "Address:")]
        [Required(ErrorMessage = "Address (street and number) required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "At least one character required.")]
        public String AddressLine { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "City required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{1,}$", ErrorMessage = "At least one character required.")]
        public String CityName { get; set; }

        [Display(Name = "PayPal mail:")]
        [Required(ErrorMessage = "PayPal required")]
        public String PayPalMail { get; set; }

        [Display(Name = "Postcode:")]
        [Required(ErrorMessage = "Postcode required")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Four digits required.")]
        public String Postcode { get; set; }

        [Display(Name = "Region:")]
        [EnumDataType(typeof(Region), ErrorMessage = "Choose region")]
        public Region Region { get; set; }

        [Display(Name = "Gender:")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Choose gender")]
        public Gender Gender { get; set; }
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
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}