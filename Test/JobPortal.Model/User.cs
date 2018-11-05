using System;
using System.Runtime.Serialization;

namespace JobPortal.Model
{
    [DataContract]
    public enum Region
    {
        Hovedstaden,
        Midtjylland,
        Nordjylland,
        Sjælland,
        Syddanmark
    }

    [DataContract]
    public enum Gender
    {
        Male,
        Female,
    }

    [DataContract]
    public class User
    {

        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual String PhoneNumber { get; set; }

        [DataMember]
        public virtual String FirstName { get; set; }

        [DataMember]
        public virtual String LastName { get; set; }

        [DataMember]
        public virtual String Email { get; set; }

        [DataMember]
        public virtual String UserName { get; set; }

        [DataMember]
        public virtual String Password { get; set; }

        [DataMember]
        public virtual String AddressLine { get; set; }

        [DataMember]
        public virtual String CityName { get; set; }

        [DataMember]
        public virtual String Postcode { get; set; }

        [DataMember]
        public virtual Region Region { get; set; }

        [DataMember]
        public virtual Gender Gender { get; set; }
        
    }
}
