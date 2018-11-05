using System;
using System.Collections.Generic;
using System.ServiceModel;
using Repositories;
using System.ServiceModel.Description;
using ServiceLibrary.Models;


namespace ServiceLibrary
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _database;


        public UserService(IRepository<Users> database)
        {
            _database = database;
        }

        public UserService()
        {

        }


        public Users CreateUser(Users u)
        {
            try
            {
                RegexMatch.DoesUserMatch(u);
                _database.Create(u);
                return u;
            }
            catch (ArgumentException)
            {
                return null;
            }

        }

        public bool DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    _database.Delete(t => t.ID == id);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool EditUser(Users u)
        {
            try
            {
                RegexMatch.DoesUserMatch(u);
                _database.Update(u);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public Users FindUser(int id)
        {
            try
            {
                Users u = _database.Get(t=> t.ID == id);
                return u;
            }
            catch
            {
                return null;

            }

        }
        public IEnumerable<Users> GetAll()
        {
            return _database.GetAll();

        }

        
    }
}
