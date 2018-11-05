using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using JobPortal.Model;
using Repository;
using Repository.DbConnection;
using ServiceLibrary.Models;
using Gender = JobPortal.Model.Gender;
using Region = JobPortal.Model.Region;

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


        public User CreateUser(User u)
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


        public IList<User> GetAll()
        {
            IList<User> modelListToReturn = new List<User>();
            IQueryable<Users> listToTransfer = _database.GetAll();
            foreach (var u in listToTransfer)
            {
                modelListToReturn.Add(new User
                {
                    ID = u.ID,
                    PhoneNumber = u.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    AddressLine = u.AddressLine,
                    Postcode = u.Postcode,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Password = u.Logging.Password,
                    UserName = u.Logging.UserName,
                    Region = (Region)Enum.Parse(typeof(Region), u.AddressTable.Region)
                });
            }
            return modelListToReturn;
            //return null;
        }

       
    }
}
