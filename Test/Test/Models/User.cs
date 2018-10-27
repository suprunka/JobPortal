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
        [DataMember]
        public virtual String PhoneNumber { get; set; }
        [DataMember]
        public String FirstName { get; set; }
        [DataMember]
        public String LastName { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public String Password { get; set; }
        [DataMember]
        public String AddressLine { get; set; }
        [DataMember]
        public String CityName { get; set; }
        [DataMember]
        public String Postcode { get; set; }
        [DataMember]
        public String Region { get; set; }
        

    }
}
