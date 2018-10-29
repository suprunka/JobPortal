using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClasses
{
   
    public class User
    {
        public virtual String PhoneNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String AddressLine { get; set; }
        public String CityName { get; set; }

        public String Postcode { get; set; }
        public String Region { get; set; }
        public Gender Gender { get; set;}
        
    }

    public enum Gender { Male, Female };
}
