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
        public virtual int ID { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[0-9]{8}$")]
        public virtual String PhoneNumber { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String FirstName { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String LastName { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public virtual String Email { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{4,}$")]
        public virtual String UserName { get; set; }

        [DataMember]
        [Required] 
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,}$")]
        public virtual String Password { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String AddressLine { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[a-zA-Z0-9ÆæØøÅå]{1,}$")]
        public virtual String CityName { get; set; }

        [DataMember]
        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Characters are not allowed.")]
        public virtual String Postcode { get; set; }

        [DataMember] 
        public virtual Region Region { get; set; }

        [DataMember]
        public virtual Gender Gender { get; set; }
    }
}
