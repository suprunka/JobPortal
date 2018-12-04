using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JobPortal.Model;

namespace WebJobPortal.Models
{
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

    public class BoughtOffers
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
        public DateTime Date { get; set; }
        public TimeSpan HourFrom { get; set; }
        public TimeSpan HourTo { get; set; }
    }

    public class AddServiceOfferModel
    {
        public ManageOffers ManageOffers { get; set; }
        public IEnumerable<WorkingHours> WorkingDays { get; set; }
    }

    public class ReviewModel
    {
        public ReviewAuthor Customer { get; set; }
        public int ServiceOfferId { get; set; }
        public string Comment { get; set; }
        public double Rate { get; set; }
    }

    public class ReviewAuthor
    {
       public Gender Gender { get; set; }
       public string Username { get; set; }
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
        public IEnumerable<TimeSpan> hoursfrom { get; set; }
        public IEnumerable<TimeSpan> hoursTo { get; set; }
        public IEnumerable<WorkingTime> Dates { get; set; }
        public Category Category { get; set; }
        public SubCategory Subcategory { get; set; }
        public ReviewModel[] Reviews { get; set; }
    }
}