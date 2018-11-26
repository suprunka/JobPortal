using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JobPortal.Model;
namespace MyWeb.Models
{

    public class ServiceOfferToOrder
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
            
        public TimeSpan From { get; set; }
            
        public TimeSpan To { get;  set; }

        public decimal RatePerHour { get; set; }
    
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }
    }


    public class ServiceOfferViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Rate per hour:")]
        [Required(ErrorMessage = "Rate per hour required")]
        [RegularExpression("^[0-9]+(\\.[0-9]{1,2})?$", ErrorMessage = "Rate per hour has be to positive number.")]
        public decimal RatePerHour { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Title required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{5,}$", ErrorMessage = "Title has to be at least 5 characters long.")]
        public string Title { get; set; }

        [Display(Name = "Descritpion:")]
        [Required(ErrorMessage = "Descritpion required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{10,}$", ErrorMessage = "Description has to be at least 10 characters long.")]
        public string Description { get; set; }

        public string Author { get; set; }
        public JobPortal.Model.Category Category { get; set; }
        public JobPortal.Model.SubCategory Subcategory { get; set; }
    }

    public class ManageOffers
    {
        public int Id { get; set; }
        [Display(Name = "Rate per hour:")]
        [Required(ErrorMessage = "Rate per hour required")]
        [RegularExpression("^[0-9]+(\\.[0-9]{1,2})?$", ErrorMessage = "Rate per hour has be to positive number.")]
        public decimal RatePerHour { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Title required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{5,}$", ErrorMessage = "Title has to be at least 5 characters long.")]
        public string Title { get; set; }

        [Display(Name = "Descritpion:")]
        [Required(ErrorMessage = "Descritpion required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{10,}$", ErrorMessage = "Description has to be at least 10 characters long.")]
        public string Description { get; set; }

        public string Author { get; set; }

        public Category Category { get; set; }

        public SubCategory Subcategory { get; set; }
    }

    public class EditOffer
    {
        public int Id { get; set; }

        [Display(Name = "Rate per hour:")]
        [Required(ErrorMessage = "Rate per hour required")]
        [RegularExpression("^[0-9]+(\\.[0-9]{1,2})?$")]
        public decimal RatePerHour { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Rate per hour required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{5,}$")]
        public string Title { get; set; }

        [Display(Name = "Descritpion:")]
        [Required(ErrorMessage = "Descritpion required")]
        [RegularExpression(" ^[a - zA - Z0 - 9ÆæØøÅå]{10,}$")]
        public string Description { get; set; }
    }

    public class AddServiceOfferModel
    {
        public ManageOffers ManageOffers { get; set; }
        public IEnumerable<WorkingHours> WorkingDays { get; set; }
    }

    public class WorkingHours
    {
        [Display(Name = "Day:")]
        public DayOfWeek NameOfDay;
        [Display(Name = "From:")]
        public TimeSpan HourFrom;
        [Display(Name = "To:")]
        public TimeSpan HourTo;
    }

    public class ViewDetails
    {
        public int Id { get; set; }
        [Display(Name = "Rate per hour:")]
        [Required(ErrorMessage = "Rate per hour required")]
        [RegularExpression("^[0-9]+(\\.[0-9]{1,2})?$", ErrorMessage = "Rate per hour has be to positive number.")]
        public decimal RatePerHour { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Title required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{5,}$", ErrorMessage = "Title has to be at least 5 characters long.")]
        public string Title { get; set; }

        [Display(Name = "Descritpion:")]
        [Required(ErrorMessage = "Descritpion required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{10,}$", ErrorMessage = "Description has to be at least 10 characters long.")]
        public string Description { get; set; }
        public string Author { get; set; }
    }
}