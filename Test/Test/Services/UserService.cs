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
using AspNetUser = Repository.DbConnection.AspNetUser;
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

        public bool CreateUser(User u, string loggingId)
        {
            try
            {
                if (RegexMatch.DoesUserMatch(u))
                {
                    _database.Create(new Users
                    {
                        AddressTable = new AddressTable
                        {
                            Postcode = u.Postcode,
                            City = u.CityName,
                            Region = u.Region.ToString(),
                        },
                        Logging_ID = loggingId,
                        Gender = new Repository.DbConnection.Gender
                        {
                            Gender1 = u.Gender.ToString(),
                        },
                        PayPalMail = u.PayPalMail,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AddressLine = u.AddressLine,
                    });
                    return true;
                }
                return false;

            }
            catch (Exception)
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
                        AspNetUser = new AspNetUser
                        {
                            UserName = u.UserName,
                            PhoneNumber = u.PhoneNumber,
                            Email = u.Email,
                            PasswordHash = u.Password,
                            EmailConfirmed = false,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnabled = false,
                            AccessFailedCount = 4,


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

        public User FindUser(string phoneNumber)
          {
              try
              {
                  var result = _database.Get(t => t.Logging_ID == phoneNumber);
                  return new User
                  {
                      ID = result.ID,
                      PhoneNumber = result.AspNetUser.PhoneNumber,
                      FirstName = result.FirstName,
                      LastName = result.LastName,
                      Email = result.AspNetUser.Email,
                      UserName = result.AspNetUser.UserName,
                     // Password = result.AspNetUsers.PasswordHash,
                      AddressLine = result.AddressLine,
                      CityName = result.AddressTable.City,
                      Postcode = result.AddressTable.Postcode,
                      PayPalMail = result.PayPalMail,
                      Region = (Region)Enum.Parse(typeof(Region), result.AddressTable.Region),
                      Gender = (Gender)Enum.Parse(typeof(Gender), result.Gender.Gender1),
                  };
              }
              catch
              {
                  return null;
              }

          }
          
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
                        PhoneNumber = result.AspNetUser.PhoneNumber,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Email = result.AspNetUser.Email,
                        UserName = result.AspNetUser.UserName,
                        //Password = result.AspNetUsers.Password,
                        AddressLine = result.AddressLine,
                        CityName = result.AddressTable.City,
                        Postcode = result.AddressTable.Postcode,
                        PayPalMail = result.PayPalMail,
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
                    PhoneNumber = u.AspNetUser.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.AspNetUser.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    // Password = u.AspNetUsers.Password,
                    UserName = u.AspNetUser.UserName,
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
                    PhoneNumber = u.AspNetUser.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.AspNetUser.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    //Password = u.AspNetUsers.Password,
                    UserName = u.AspNetUser.UserName,
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
                    PhoneNumber = u.AspNetUser.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.AspNetUser.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    //Password = u.AspNetUsers.Password,
                    UserName = u.AspNetUser.UserName,
                    Region = (Region)Enum.Parse(typeof(Region), u.AddressTable.Region)
                });
            }
            return resultToReturn.ToArray();
        }



        private AspNetUser Register(String login, String password, string mail, string phonenumber)
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
            var pass = Encrypt.EncryptString(System.Text.Encoding.UTF8.GetString(result));
            AspNetUser logging = new AspNetUser
            {
                Id= login+"."+phonenumber,
                Email = mail,
                PhoneNumber = phonenumber,
                UserName = login,
                PasswordHash = pass,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 4,
            };
            return logging;

        }

        public bool Login(String login, String password)
        {

            var existing = _database.Login(new AspNetUser { UserName = login, PasswordHash = password });
            if (existing != null)
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
      

       
    }
}
