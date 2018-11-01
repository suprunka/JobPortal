using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebJobPortal.ServiceReference1;
using ServiceLibrary;

namespace WebJobPortal.Models
{
    public class UserModel
    {
        public readonly ServiceLibrary.IUserService Object;

        [Required]
        [RegularExpression("^[0-9]{8}$")]
        public virtual String PhoneNumber { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String FirstName { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String LastName { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public String Email { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String UserName { get; set; }

        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,}$")]
        public String Password { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String AddressLine { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String CityName { get; set; }

        [Required]
        [RegularExpression("^[0-9]{4}$")]
        public String Postcode { get; set; }

        public Region Region { get; set; }

        public Gender Gender { get; set; }
    }
}