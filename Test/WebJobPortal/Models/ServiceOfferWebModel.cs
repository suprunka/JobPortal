using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebJobPortal.Models
{
    public class ServiceOfferWebModel
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

        public string AuthorNumber { get; set; }
        public Category Category { get; set; }
        public SubCategory Subcategory { get; set; }
    }
}