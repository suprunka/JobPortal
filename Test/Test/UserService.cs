﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Repositories;
using ServiceLibrary.DbConnection;
using ServiceLibrary.Models;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class UserService : IUserService
    {
        private readonly IRepository<User> _database;

        public UserService(IRepository<User> database)
        {
            _database = database;
        }

        public User CreateUser(User u)
        {
            if (u != null)
            {
               

                


          
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