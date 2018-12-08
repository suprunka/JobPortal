using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
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
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserService()
        {
            _unitOfWork = new UnitOfWork(new JobPortalDatabaseDataContext());
        }

        public bool CreateUser(User u, string loggingId)
        {
            try
            {
                if (RegexMatch.DoesUserMatch(u))
                {
                    _unitOfWork.Users.Create(new Users
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
                        Description = u.Description,
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
                    _unitOfWork.Users.Delete(t => t.ID == id);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public bool EditUserEmail(User u)
        {
            if (RegexMatch.DoesUserEmailMatch(u))
            {
                _unitOfWork.Users.UpdateUserMail(new Users
                {
                    ID = u.ID,
                    AspNetUsers = new AspNetUsers
                    {
                        Email = u.Email,
                    }

                });
                return true;
            }
            else
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
                    _unitOfWork.Users.Update(new Users
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
                        PayPalMail = u.PayPalMail,
                        LastUpdate = u.LastUpdate

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
                var result = _unitOfWork.Users.Get(t => t.Logging_ID == phoneNumber);
                return new User
                {
                    ID = result.ID,
                    PhoneNumber = result.AspNetUsers.PhoneNumber,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.AspNetUsers.Email,
                    UserName = result.AspNetUsers.UserName,
                    AddressLine = result.AddressLine,
                    CityName = result.AddressTable.City,
                    Postcode = result.AddressTable.Postcode,
                    PayPalMail = result.PayPalMail,
                    Region = (Region)Enum.Parse(typeof(Region), result.AddressTable.Region),
                    Gender = (Gender)Enum.Parse(typeof(Gender), result.Gender.Gender1),
                    Description = result.Description,
                    LastUpdate = result.LastUpdate,
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
                    var result = _unitOfWork.Users.Get(t => t.ID == id);
                    if (result != null)
                    {
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
                            PayPalMail = result.PayPalMail,
                            Region = (Region)Enum.Parse(typeof(Region), result.AddressTable.Region),
                            Gender = (Gender)Enum.Parse(typeof(Gender), result.Gender.Gender1),
                            Description = result.Description,
                            LastUpdate = result.LastUpdate,

                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch
            {
                throw new InvalidOperationException();
            }

        }

        public User[] GetAll()
        {
            IList<User> resultToReturn = new List<User>();
            foreach (var u in _unitOfWork.Users.GetAll())
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
                    LoggingId = u.Logging_ID,
                    // Password = u.AspNetUsers.Password,
                    UserName = u.AspNetUsers.UserName,
                    Description = u.Description,
                    PayPalMail = u.PayPalMail,
                    Region = (Region)Enum.Parse(typeof(Region), u.AddressTable.Region),
                    LastUpdate = u.LastUpdate
                });
            }
            return resultToReturn.ToArray();
        }

        public User[] ListByGender(Gender gender)
        {
            IList<User> resultToReturn = new List<User>();
            foreach (var u in _unitOfWork.Users.List(Users => Users.Gender.Gender1 == gender.ToString()))
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
            foreach (var u in _unitOfWork.Users.List(Users => Users.AddressTable.Region == region.ToString()))
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

        public bool AddDescription(User u)
        {
            _unitOfWork.Users.AddDescription(new Users
            {
                ID = u.ID,
                Description = u.Description
            });
            return true;
        }
    }
}
