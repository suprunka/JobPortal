using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DevTrends.WCFDataAnnotations;
using Repositories;
using ServiceLibrary.DbConnection;
using ServiceLibrary.Models;

namespace ServiceLibrary
{
    [ValidateDataAnnotationsBehavior]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UserService : IUserService
    {
        private readonly IRepository<User> _database;

        public UserService(IRepository<User> database)
        {
            _database = database;
        }
        public UserService()
        {
           
        }

        public User CreateUser(User u)
        {
            if (u != null)
            {
                try
                {
                    _database.Create(u);
                    return u;
                }catch(ArgumentNullException)
                {
                   
                }
            }
            return null;
        }

        public bool DeleteUser(String phoneNumber)
        {
            if (phoneNumber.Length > 0)
            {

             
            }
            return false;
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
