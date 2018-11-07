using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
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


        public bool CreateUser(User u)
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
                        Logging = new Logging
                        {
                            Password = u.Password,
                            UserName = u.UserName,
                        },
                        Gender = new Repository.DbConnection.Gender
                        {
                            Gender1 = u.Gender.ToString(),
                        },

                        PhoneNumber = u.PhoneNumber,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        AddressLine = u.AddressLine,

                    });
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
                        Logging = new Logging
                        {
                            Password = u.Password,
                            UserName = u.UserName,
                        },
                        Gender = new Repository.DbConnection.Gender
                        {
                            Gender1 = u.Gender.ToString(),
                        },

                        PhoneNumber = u.PhoneNumber,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
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

        public bool EditWebUser(User u)
        {
            try
            {
                if (RegexMatch.DoesWebUserMatch(u))
                {
                    _database.UpdateWeb(new Users
                    {
                        AddressTable = new AddressTable
                        {
                            Postcode = u.Postcode,
                            City = u.CityName,
                            Region = u.Region.ToString(),
                        },

                        Gender = new Repository.DbConnection.Gender
                        {
                            Gender1 = u.Gender.ToString(),
                        },
                        ID = u.ID,
                        PhoneNumber = u.PhoneNumber,
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
                var result = _database.Get(t => t.PhoneNumber == phoneNumber);
                return new User
                {
                    ID = result.ID,
                    PhoneNumber = result.PhoneNumber,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    UserName = result.Logging.UserName,
                    Password = result.Logging.Password,
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



        public User[] GetAll()
        {
            IList<User> resultToReturn = new List<User>();
            foreach (var u in _database.GetAll())
            {
                resultToReturn.Add(new User
                {
                    ID = u.ID,
                    PhoneNumber = u.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    Password = u.Logging.Password,
                    UserName = u.Logging.UserName,
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
                    PhoneNumber = u.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    Password = u.Logging.Password,
                    UserName = u.Logging.UserName,
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
                    PhoneNumber = u.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    AddressLine = u.AddressLine,
                    Gender = (Gender)Enum.Parse(typeof(Gender), u.Gender.Gender1),
                    CityName = u.AddressTable.City,
                    Postcode = u.AddressTable.Postcode,
                    Password = u.Logging.Password,
                    UserName = u.Logging.UserName,
                    Region = (Region)Enum.Parse(typeof(Region), u.AddressTable.Region)
                });
            }
            return resultToReturn.ToArray();
        }
    }
}
