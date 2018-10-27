using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClasses
{
    public class User
    {
        String PhoneNumber { get; set; }
        String FirstName { get; set; }
        String LastName { get; set; }
        String Email { get; set; }
        String UserName { get; set; }
        String Password { get; set; }
        String AddressLine { get; set; }
        String CityName { get; set; }
        String Postcode { get; set; }
        String Region { get; set; }
        Gender Gender { get; set;}
        
    }

    enum Gender { Male, Female };
}
