using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ServiceLibrary.DAO
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string AddressLine { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Postcode { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public GenderEnum Gender { get; set; }
        [DataMember]
        public string Linkdin { get; set; }


    }
}
