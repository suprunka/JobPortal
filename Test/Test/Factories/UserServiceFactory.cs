using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ModelClasses;

namespace ServiceLibrary.Factories
{
    public static class UserServiceFactory
    {
        public static IUserService GetUserService()
        {
            var shouldMock = bool.Parse(ConfigurationManager.AppSettings["MockUserService"]);
            if (!shouldMock)
            {
                return new UserService();
            }
            return new UserServiceMock();
        }


        public class UserServiceMock : IUserService
        {
            public User CreateUser(User u)
            {
                throw new NotImplementedException();
            }

            public bool DeleteUser(string PhoneNumber)
            {
                throw new NotImplementedException();
            }

            public bool EditUser(User u)
            {
                throw new NotImplementedException();
            }

            public User FindUser(string PhoneNumber)
            {
                throw new NotImplementedException();
            }
        }
    }
}
