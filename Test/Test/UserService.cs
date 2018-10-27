using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ModelClasses;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class UserService : IUserService
    {
        public User CreateUser(User u)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(String PhoneNumber)
        {
            throw new NotImplementedException();
        }

        public bool EditUser(User u)
        {
            throw new NotImplementedException();
        }

        public User FindUser(String PhoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
