using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JobPortal.Model;
namespace WebJobPortal.Models
{
    public class ServiceOfferViewModel
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

        public string Author { get; set; }
        public JobPortal.Model.Category Category { get; set; }
        public JobPortal.Model.SubCategory Subcategory { get; set; }
    }
    public class ManageOffers
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

        public string Author { get; set; }
        public Category Category { get; set; }
        public SubCategory Subcategory { get; set; }

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
        public NodaTime.LocalTime HourFrom;
        [Display(Name = "To:")]
        public NodaTime.LocalTime HourTo;
    }
}