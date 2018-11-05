using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AppJobPortal.Mapping;
using Repositories;
using Repository;
using Repository.DbConnection;
using ServiceLibrary.Models;
using static Repository.UsersRepository;
using User = ServiceLibrary.Models.Users;

namespace ServiceLibrary
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UserService : IUserService
    {
        private readonly IUserRepository _database;


        public UserService(IUserRepository database)
        {
            _database = database;
        }
        public UserService()
        {
            _database = new UsersRepository(new JobPortalDatabaseDataContext());
        }


        public User CreateUser( RepositoryUser u)
        {
            try
            {
                _database.Create(u);
                return AutoMapper.Mapper.Map(u, new User());
                   
            }
            catch (ArgumentNullException)
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

        public bool EditUser(User u)
        {
            //_database.Update(u);
            return true;
        }

        public User FindUser(int id)
        {
            try
            {
                // User u = _database.Get(t=> t.ID == id);
                // return u;
                return null;
            }
            catch
            {
                return null;

            }

        }


        public IQueryable<User> GetAll()
        {
             return _database.GetAll().Cast<User>();
            //return null;
        }

       
    }
}
