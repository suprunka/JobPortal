﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Linq;
using System.Linq;
using Repository;
using UnitTestProject1.Database_tests;
using AddressTables = Repository.DbConnection.AddressTable;
using Users = Repository.DbConnection.Users;
using AspNetUsers = Repository.DbConnection.AspNetUsers;
using Gender = Repository.DbConnection.Gender;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class RepositoryTest
    {
        //Creating users for test purpose
        #region
        private static Users GetUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTables
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                AspNetUsers = new AspNetUsers
                {
                    Id= "Username12",
                    PasswordHash = "Adama1",
                    UserName = "Username12",
                    PhoneNumber = "12345670",
                    Email = "adam2@gmail.com",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 4,
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },
                PayPalMail = "mama@wp.pl",
                FirstName = "Adam",
                LastName = "Adam",
                AddressLine = "mickiewicza",

            };
            return userStub;

        }
        //same as GetUser() [same  phonenumber]
        private static Users GetAnotherUser()
        {
            var userStub = new Users
            {
                
                AddressTable = new AddressTables
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midjylland"
                },
                AspNetUsers = new AspNetUsers
                {
                    Id= "Username12",
                    PasswordHash = "Adama1",
                    UserName = "Username12",
                    PhoneNumber = "12345670",
                    Email = "adam2@gmail.com",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 4,
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },
                PayPalMail = "mama@wp.pl",
                FirstName = "Adam",
                LastName = "Adam",
                AddressLine = "mickiewicza",

            };
            return userStub;

        }
        private static Users ToUpdate()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTables
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midtjylland"
                },
                AspNetUsers = new AspNetUsers
                {
                    Id = "Username12",
                    PasswordHash = "Adama1",
                    UserName = "Username12",
                    PhoneNumber = "12345670",
                    Email = "adam2@gmail.com",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 4,
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },
                PayPalMail = "lal@wp.pl",
                FirstName = "Updated",
                LastName = "Adam",
                AddressLine = "mickiewicza",

            };
            return userStub;

        }

        private static Users ForthUser()
        {
            var userStub = new Users
            {

                     AddressTable = new AddressTables
                     {
                         Postcode = "9000",
                         City = "Aalborg",
                         Region = "Nordjylland"
                     },
                     AspNetUsers = new AspNetUsers
                     {
                         Id = "Username123",
                         PasswordHash = "Adama1",
                         UserName = "Username12",
                         PhoneNumber = "12345670",
                         Email = "adam2@gmail.com",
                         EmailConfirmed = false,
                         PhoneNumberConfirmed = false,
                         TwoFactorEnabled = false,
                         LockoutEnabled = false,
                         AccessFailedCount = 4,
                     },
                     Gender = new Gender
                     {
                         Gender1 = "Male",
                     },
                     PayPalMail = "mama@wp.pl",
                     FirstName = "Adam",
                     LastName = "Adam",
                     AddressLine = "mickiewicza",

                 };
            return userStub;

        }
        private static Users ThirdUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTables
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                AspNetUsers = new AspNetUsers
                {
                    Id = "UsernameThird",
                    PasswordHash = "Adama1",
                    UserName = "UsernameThird",
                    PhoneNumber = "09098999",
                    Email = "UsernameThird@gmail.com",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 4,
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },
                PayPalMail = "mama@wp.pl",
                FirstName = "Adam2",
                LastName = "Adam2",
                AddressLine = "mickiewicza",

            };
            return userStub;
        }
        #endregion


        //Testing adding a valid object to a database. [OK]
        [TestMethod]
        public void Add_TestClassUserObjectPassed()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.Users.Create(GetUser(),null);

                Assert.IsNotNull(result);

                Assert.IsNotNull(context.Users.FirstOrDefault());
                context.Users.DeleteAllOnSubmit(context.Users);
                context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                context.AddressTables.DeleteAllOnSubmit(context.AddressTables);
                context.SubmitChanges();
            }

        }

        //While creating two users with same address only one address row is created [OK]
        [TestMethod]
        public void Creates_One_Address_Record_If_Two_Users_Share_The_Same_Address()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser(), null);
                    unitOfWork.Users.Create(ThirdUser(), null);
                    int numberOfAddressRecords = context.AddressTables.Count();
                    Assert.AreEqual(1, numberOfAddressRecords);

                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Testing adding two same object which should be deteced by database and second object shouldn't be added. [OK]
        [TestMethod]
        public void Adding_Two_Same_Object_Throws_Exception()
        {
            var context = new DbTestDataContext();

            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser(), null);
                    unitOfWork.Users.Create(GetAnotherUser());
                }
                catch (DuplicateKeyException)
                {
                    int numberOfUsers = context.Users.Count();
                    int numberOfAddresses = context.AddressTables.Count();
                    int numberOfAspNetUserss = context.AspNetUsers.Count();
                    Assert.AreEqual(1, numberOfUsers);
                    Assert.AreEqual(1, numberOfAddresses);
                    Assert.AreEqual(1, numberOfAspNetUserss);
                }
            }

            var secondContext = new DbTestDataContext();
             using (var unitOfWork = new UnitOfWork(secondContext))
             {
                secondContext.ServiceOffers.DeleteAllOnSubmit(secondContext.ServiceOffers);
                 secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                 secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                 secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                 secondContext.SubmitChanges();
             }
        }

        //While adding two same object only first of them is saved in database. [OK]
        [TestMethod]
        public void Adding_Two_Same_Adds_First_Of_Them()
        {
            var context = new DbTestDataContext();

            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var result = unitOfWork.Users.Create(GetUser(), null);
                    Assert.IsNotNull(result);
                    unitOfWork.Users.Create(GetUser(), null);
                }
                catch (DuplicateKeyException)
                {

                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.ServiceOffers.DeleteAllOnSubmit(secondContext.ServiceOffers);
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Testing deleting an object from database.
        [TestMethod]
        public void Deleting_Object_From_Database()
        {
            var context = new DbTestDataContext();


            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var x = unitOfWork.Users.Create(GetUser(), null);
                    bool result = unitOfWork.Users.Delete(t => t.ID == x.ID);
                    Assert.IsTrue(result);

                }
                catch
                {
                  
                    Assert.Fail();
                }
            }



            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Testing deleting an object, however if there is more references to an address table the row remains.
        [TestMethod]
        public void Deleting_Object_But_Dont_Delete_Address_Records_If_There_Is_One_More_Person_Using_It()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var user = unitOfWork.Users.Create(GetUser(), null);
                    unitOfWork.Users.Create(ThirdUser(), null);
                    unitOfWork.Users.Delete(t => t.ID == user.ID);
                    Assert.IsNotNull(context.AddressTables.First());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Testing deleting an object and deleting address table if there in no one referencing it
        [TestMethod]
        public void Deleting_User_And_Address_Table_If_No_One_Uses_It()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var user = unitOfWork.Users.Create(GetUser(), null);
                    unitOfWork.Users.Delete(t => t.ID == user.ID);
                    Assert.AreEqual(0, context.AddressTables.Count());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Testing returning all users in a database. 
        [TestMethod]
        public void Get_All_Users_From_Database()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser(), null);
                    unitOfWork.Users.Create(ThirdUser());
                    IQueryable<Users> listOfAvailableUsers = unitOfWork.Users.GetAll();
                    Assert.AreEqual(2, listOfAvailableUsers.ToArray().Count());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Returns a specific user with all information from different tables.
        [TestMethod]
        public void Get_Specific_User_With_All_Information()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser(), null);
                    Users found = unitOfWork.Users.Get(t => t.AspNetUsers.PhoneNumber == "12345670");
                    Assert.AreEqual("adam2@gmail.com", found.AspNetUsers.Email);
                    Assert.AreEqual("Male", found.Gender.Gender1);
                    Assert.AreEqual("Username12", found.AspNetUsers.UserName);
                    Assert.AreEqual("Nordjylland", found.AddressTable.Region);
                }
                catch
                {
                    Assert.Fail();

                }
            }
                    
            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Filters database of users and returns ones who fulfil specific requirements.
        [TestMethod]
        public void Filtr_Database_Of_Users()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser(), null);
                    unitOfWork.Users.Create(ForthUser(), null);
                    unitOfWork.Users.Create(ThirdUser(), null);
                    IQueryable<Users> filtredList = unitOfWork.Users.List(u => u.AddressTable.City == "Aalborg");
                    Assert.AreEqual(3, filtredList.ToArray().Count());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }

        //Testing an edition of existing user in a database
        [TestMethod]
        public void Edition_Of_User_Is_Saved()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    Users u = unitOfWork.Users.Create(GetUser(), null);
                    Users toUpdate = ToUpdate();
                    toUpdate.ID = u.ID;

                    bool edited = unitOfWork.Users.Update(toUpdate);
                    Assert.IsTrue(edited);
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTables.DeleteAllOnSubmit(secondContext.AddressTables);
                secondContext.SubmitChanges();
            }
        }
    }
}

