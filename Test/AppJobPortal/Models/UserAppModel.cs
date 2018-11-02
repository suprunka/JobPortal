
using AppJobPortal.UserServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJobPortal.Models
{
    public class UserAppModel
    {
        

        public UserAppModel(string phoneNumber, string firstName, string lastName, string email, string userName, string password,
            string addressLine, string cityName, string postCode, Region region, Gender gender)
        {
           PhoneNumber = phoneNumber;
           FirstName = firstName;
           LastName = lastName;
           Email = email;
           UserName = userName;
           Password = password;
           AddressLine = addressLine;
           CityName = cityName;
           Postcode = postCode;
           Region = region;
           Gender = gender;
        }

        public UserAppModel()
        {

        }

        public virtual int ID { get; set; }

        public virtual String PhoneNumber { get; set; }

        public virtual String FirstName { get; set; }

        public virtual String LastName { get; set; }

        public virtual String Email { get; set; }

        public virtual String UserName { get; set; }

        public virtual String Password { get; set; }

        public virtual String AddressLine { get; set; }

        public virtual String CityName { get; set; }

        public virtual String Postcode { get; set; }

        public virtual Region Region { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
