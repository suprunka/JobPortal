using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        [Required]
        [RegularExpression ("^[0-9]{8}$")]
        public virtual String PhoneNumber { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")] //allows danish characters
        public String FirstName { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String LastName { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public String Email { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{4,}$")]
        public String UserName { get; set; }

        [DataMember]
        [Required] 
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,}$")]
        public String Password { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String AddressLine { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public String CityName { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[0-9]{4}$")]
        public String Postcode { get; set; }

        [DataMember] //enum
        public Region Region { get; set; }

        [DataMember]
        public Gender Gender { get; set; }
    }
}
