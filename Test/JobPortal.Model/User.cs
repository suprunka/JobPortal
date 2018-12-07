using System;
using System.Data.Linq;
using System.Runtime.Serialization;

namespace JobPortal.Model
{
    [DataContract(Name = "Region")]
    public enum Region
    {
        [EnumMember]
        Hovedstaden,
        [EnumMember]
        Midtjylland,
        [EnumMember]
        Nordjylland,
        [EnumMember]
        Sjalland,
        [EnumMember]
        Syddanmark
    }

    [DataContract(Name ="Gender")]
    public enum Gender
    {
        [EnumMember]
        Male,
        [EnumMember]
        Female,
    }
   
    [DataContract]
    [KnownType(typeof(Gender))]
    [KnownType(typeof(Region))]
    public class User
    {

        [DataMember]
        public virtual String Description { get; set; }
        [DataMember]
        public virtual Binary LastUpdate { get; set; }

        [DataMember]
        public virtual int ID { get; set; }
        [DataMember]
        public virtual string LoggingId { get; set; }

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

        [DataMember]
        public virtual string PayPalMail { get; set; }

        [DataMember]
        public virtual ShoppingCard ShoppingCard { get; set; } 
    }
}
