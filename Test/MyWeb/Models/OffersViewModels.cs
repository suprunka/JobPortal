using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JobPortal.Model;

namespace WebJobPortal.Models
{
    public class ManageOfferModel
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
     
        public string Description { get; set; }
        public string Author { get; set; }
        public Category Category { get; set; }
        public SubCategory Subcategory { get; set; }
    }

    public class BoughtOfferModel
    {
        public int Id { get; set; }
        [Display(Name = "Rate per hour:")]
        [Required(ErrorMessage = "Rate per hour required")]
        [RegularExpression("^[0-9]+(\\.[0-9]{1,2})?$", ErrorMessage = "Rate per hour has be to positive number.")]

        public string Author { get; set; }

        [Display(Name = "Descritpion:")]
        [Required(ErrorMessage = "Descritpion required")]
        public string Description { get; set; }

        public decimal RatePerHour { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Title required")]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå ]{5,}$", ErrorMessage = "Title has to be at least 5 characters long.")]
        public string Title { get; set; }

        public SubCategory Subcategory { get; set; }

        public Category Category { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan HourFrom { get; set; }

        public TimeSpan HourTo { get; set; }
    }

    public class AddOfferModel
    {
        public ManageOfferModel ManageOffers { get; set; }
        public IEnumerable<WorkingHoursOfOfferModel> WorkingDays { get; set; }
    }

    public class ReviewModel
    {
        public ReviewAuthorViewModel Customer { get; set; }
        public int ServiceOfferId { get; set; }
        public string Comment { get; set; }
        public double Rate { get; set; }
    }

    public class WorkingHoursOfOfferModel
    {
        [Display(Name = "Day:")]
        public DayOfWeek NameOfDay;
        [Display(Name = "From:")]
        public TimeSpan HourFrom;
        [Display(Name = "To:")]
        public TimeSpan HourTo;
    }

    public class ViewDetailsModel
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
        
        public string Description { get; set; }

        public string Author { get; set; }

        public IEnumerable<WorkingTime> Dates { get; set; }

        public Category Category { get; set; }

        public SubCategory Subcategory { get; set; }

        public ReviewModel[] Reviews { get; set; }
    }

    public class JobOfferViewModel
    {
        public DateTime CurrentDate { get; set; }
        public CustomerViewModel Customer { get; set; }
        public decimal TotalPrice { get; set; }
        public BoughtOfferModel Offer { get; set; }
    }
}