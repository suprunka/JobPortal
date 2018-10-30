using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    [DataContract]
    public class User
    {
        [DataMember] // should have 8 digit
        public virtual String PhoneNumber { get; set; } 
        [DataMember] //include english alphabet and danish signs
        public String FirstName { get; set; }
        [DataMember] //same as first name
        public String LastName { get; set; }
        [DataMember] //have '@' sign
        public String Email { get; set; }
        [DataMember] //
        public String UserName { get; set; }
        [DataMember] //have capital, small letters and number
        public String Password { get; set; }
        [DataMember] 
        public String AddressLine { get; set; }
        [DataMember]
        public String CityName { get; set; }
        [DataMember] //should be 4 digit
        public String Postcode { get; set; }
        [DataMember] //enum
        public Region Region { get; set; }
        [DataMember]
        public Gender Gender { get; set; }
    }
}
