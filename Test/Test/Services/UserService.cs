using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using JobPortal.Model;
using Repository;
using Repository.DbConnection;
using Repository.DbConnection.Entity;
using ServiceLibrary.Models;
using Gender = JobPortal.Model.Gender;
using Region = JobPortal.Model.Region;

namespace ServiceLibrary
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UserService : IUserService
    {
        private readonly IUserRepository _database;
        private readonly dmai0917_1067677Entities1 _logEntity;

        public UserService(IUserRepository database)
        {
            _database = database;
        }

        public UserService()
        {
            _database = new UsersRepository(new JobPortalDatabaseDataContext());
        }

        public bool CreateUser(User u, string loggingId)
        {
            try
            {
                if (RegexMatch.DoesUserMatch(u))
                {
                    AspNetUsers aspNetUsers = null;
                    if (loggingId != null)
                    {
                        aspNetUsers = _database.Get(x => x.AspNetUsers.Id == loggingId).AspNetUsers;
                    }
                    else
                    {
                        aspNetUsers = Register(u.UserName, u.Password, u.Email, u.PhoneNumber);
                    }
                    _database.Create(new Users
                    {
                        AddressTable = new AddressTable
                        {
                            Postcode = u.Postcode,
                            City = u.CityName,
                            Region = u.Region.ToString(),
                        },
                        AspNetUsers = aspNetUsers,
                    Gender = new Repository.DbConnection.Gender
                        {
                            Gender1 = u.Gender.ToString(),
                        },

                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AddressLine = u.AddressLine,

                    }
                    );
                    return true;
                }
                return false;

            }
            catch (DuplicateKeyException)
            {
                return false;
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
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public bool EditUser(User u)
        {
            try
            {
                if (RegexMatch.DoesUserMatch(u))
                {
                    _database.Update(new Users
                    {

                        AddressTable = new AddressTable
                        {
                            Postcode = u.Postcode,
                            City = u.CityName,
                            Region = u.Region.ToString(),
                        },
                        AspNetUsers = new AspNetUsers
                        {
                            UserName = u.UserName,
                            PhoneNumber = u.PhoneNumber,
                            Email = u.Email,

                        },
                        Gender = new Repository.DbConnection.Gender
                        {
                            Gender1 = u.Gender.ToString(),
                        },
                        ID = u.ID,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AddressLine = u.AddressLine,

                    });
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

      /*  public User FindUser(string phoneNumber)
        {
            try
            {
                var result = _database.Get(t => t.PhoneNumber == phoneNumber);
                return new User
                {
                    ID = result.ID,
                    PhoneNumber = result.PhoneNumber,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    UserName = result.AspNetUsers.UserName,
                    Password = result.AspNetUsers.Password,
                    AddressLine = result.AddressLine,
                    CityName = result.AddressTable.City,
                    Postcode = result.AddressTable.Postcode,
                    Region = (Region)Enum.Parse(typeof(Region), result.AddressTable.Region),
                    Gender = (Gender)Enum.Parse(typeof(Gender), result.Gender.Gender1),
                };
            }
            catch
            {
                return null;
            }

        }
        */
        public User FindUserByID(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = _database.Get(t => t.ID == id);
                    return new User
                    {
                        ID = result.ID,
                        PhoneNumber = result.AspNetUsers.PhoneNumber,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Email = result.AspNetUsers.Email,
                        UserName = result.AspNetUsers.UserName,
                        //Password = result.AspNetUsers.Password,
                        AddressLine = result.AddressLine,
                        CityName = result.AddressTable.City,
                        Postcode = result.AddressTable.Postcode,
                        Region = (Region)Enum.Parse(typeof(Region), result.AddressTable.Region),
                        Gender = (Gender)Enum.Parse(typeof(Gender), result.Gender.Gender1),
                    };
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public User[] GetAll()
        {
            IList<User> resultToReturn = new List<User>();
            foreach (var u in _database.GetAll())
            {
                resultToReturn.Add(new User
                {
                    ID = u.ID,
                    PhoneNumber = u.AspNetUsers.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.AspNetUsers.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                   // Password = u.AspNetUsers.Password,
                    UserName = u.AspNetUsers.UserName,
                    Region = (Region)Enum.Parse(typeof(Region), u.AddressTable.Region)
                });
            }
            return resultToReturn.ToArray();
        }

        public User[] ListByGender(Gender gender)
        {
            IList<User> resultToReturn = new List<User>();
            foreach (var u in _database.List(Users => Users.Gender.Gender1 == gender.ToString()))
            {
                resultToReturn.Add(new User
                {
                    ID = u.ID,
                    PhoneNumber = u.AspNetUsers.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.AspNetUsers.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    //Password = u.AspNetUsers.Password,
                    UserName = u.AspNetUsers.UserName,
                    Region = (Region)Enum.Parse(typeof(Region), u.AddressTable.Region)
                });
            }
            return resultToReturn.ToArray();
        }

        public User[] ListByRegion(Region region)
        {
            IList<User> resultToReturn = new List<User>();
            foreach (var u in _database.List(Users => Users.AddressTable.Region == region.ToString()))
            {
                resultToReturn.Add(new User
                {
                    ID = u.ID,
                    PhoneNumber = u.AspNetUsers.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.AspNetUsers.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    //Password = u.AspNetUsers.Password,
                    UserName = u.AspNetUsers.UserName,
                    Region = (Region)Enum.Parse(typeof(Region), u.AddressTable.Region)
                });
            }
            return resultToReturn.ToArray();
        }

       

        private AspNetUsers Register(String login, String password, string mail, string phonenumber)
        {
            if (login.Length == 0 || password.Length == 0)
            {
                return null; 
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            AspNetUsers logging = new AspNetUsers
            {
                Email = mail,
                PhoneNumber = phonenumber,
                UserName = login,
                PasswordHash = Encrypt.EncryptString(System.Text.Encoding.UTF8.GetString(result))
            };
            return logging;
        
        }

        public bool Login(String login, String password)
        {

            var existing = _database.Login(new AspNetUsers { UserName = login, PasswordHash = password });
            if (existing!=  null)
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                byte[] result = md5.Hash;
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    str.Append(result[i].ToString("x2"));
                }
                String t = Encoding.UTF8.GetString(result);
                if (t.Equals(Encrypt.DecryptString(existing.PasswordHash)))
                {
                    return true;
                }
            }
            return false;
        }
        public dmai0917_1067677Entities1 GetLoginEntity()
        {
            return dmai0917_1067677Entities1.Create();
        }

        public User FindUser(string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
