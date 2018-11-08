using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Linq;
using System.Linq;
using Repository;
using UnitTestProject1.Database_tests;
using AddressTable = Repository.DbConnection.AddressTable;
using Users = Repository.DbConnection.Users;
using Logging = Repository.DbConnection.Logging;
using Gender = Repository.DbConnection.Gender;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class RepositoryTest
    {
        /*//Creating users for test purpose
        #region
        private static Users GetUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username1",
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            };
            return userStub;

        }
        //same as GetUser() [same username and phonenumber]
        private static Users GetAnotherUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTable
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username21",
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            };
            return userStub;

        }
        private static Users ToUpdate()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTable
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username1",
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adamUpdated",
                AddressLine = "Updated",

            };
            return userStub;

        }

        private static Users ForthUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTable
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username100",
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

                PhoneNumber = "35896452",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            };
            return userStub;

        }
        private static Users ThirdUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username3",
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

                PhoneNumber = "15975384",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",


            };
            return userStub;
        }
        #endregion


        //Testing adding a valid object to a database. [OK]
        [TestMethod]
        public void Add_TestClassUserObjectPassed()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.Users.Create(GetUser());
                Assert.IsNotNull(context.Users.FirstOrDefault(t => t.PhoneNumber == "12345678"));
                context.Users.DeleteAllOnSubmit(context.Users);
                context.Logging.DeleteAllOnSubmit(context.Logging);
                context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                context.SubmitChanges();
            }

        }

        //While creating two users with same address only one address row is created [OK]
        [TestMethod]
        public void Creates_One_Address_Record_If_Two_Users_Share_The_Same_Address()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(ThirdUser());
                    int numberOfAddressRecords = context.AddressTable.Count();
                    Assert.AreEqual(1, numberOfAddressRecords);

                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Testing adding two same object which should be deteced by database and second object shouldn't be added. [OK]
        [TestMethod]
        public void Adding_Two_Same_Object_Throws_Exception()
        {
            var context = new JobPortalTestDBDataContext();

            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(GetAnotherUser());
                }
                catch (DuplicateKeyException)
                {
                    int numberOfUsers = context.Users.Count();
                    int numberOfAddresses = context.AddressTable.Count();
                    int numberOfLoggings = context.Logging.Count();
                    Assert.AreEqual(1, numberOfUsers);
                    Assert.AreEqual(1, numberOfAddresses);
                    Assert.AreEqual(1, numberOfLoggings);
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
             using (var unitOfWork = new UnitOfWork(secondContext))
             {
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                 secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                 secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                 secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                 secondContext.SubmitChanges();
             }
        }

        //While adding two same object only first of them is saved in database. [OK]
        [TestMethod]
        public void Adding_Two_Same_Adds_First_Of_Them()
        {
            var context = new JobPortalTestDBDataContext();

            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var result = unitOfWork.Users.Create(GetUser());
                    Assert.IsNotNull(result);
                    unitOfWork.Users.Create(GetUser());
                }
                catch (DuplicateKeyException)
                {

                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Testing deleting an object from database.
        [TestMethod]
        public void Deleting_Object_From_Database()
        {
            var context = new JobPortalTestDBDataContext();


            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    bool result = unitOfWork.Users.Delete(t => t.PhoneNumber == GetUser().PhoneNumber);
                    Assert.IsTrue(result);

                }
                catch
                {
                    Assert.Fail();
                }
            }



            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Testing deleting an object, however if there is more references to an address table the row remains.
        [TestMethod]
        public void Deleting_Object_But_Dont_Delete_Address_Records_If_There_Is_One_More_Person_Using_It()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(ThirdUser());
                    unitOfWork.Users.Delete(t => t.PhoneNumber == GetUser().PhoneNumber);
                    Assert.IsNotNull(context.AddressTable.First());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Testing deleting an object and deleting address table if there in no one referencing it
        [TestMethod]
        public void Deleting_User_And_Address_Table_If_No_One_Uses_It()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Delete(t => t.PhoneNumber == GetUser().PhoneNumber);
                    Assert.AreEqual(0, context.AddressTable.Count());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Testing returning all users in a database. 
        [TestMethod]
        public void Get_All_Users_From_Database()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(ThirdUser());
                    IQueryable<Users> listOfAvailableUsers = unitOfWork.Users.GetAll();
                    Assert.AreEqual(2, listOfAvailableUsers.ToArray().Count());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Returns a specific user with all information from different tables.
        [TestMethod]
        public void Get_Specific_User_With_All_Information()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    Users found = unitOfWork.Users.Get(t => t.PhoneNumber == "12345678");
                    Assert.AreEqual("adam@gmail.com", found.Email);
                    Assert.AreEqual("Male", found.Gender.Gender1);
                    Assert.AreEqual("Username1", found.Logging.UserName);
                    Assert.AreEqual("Nordjylland", found.AddressTable.Region);
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Filters database of users and returns ones who fulfil specific requirements.
        [TestMethod]
        public void Filtr_Database_Of_Users()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(ForthUser());
                    unitOfWork.Users.Create(ThirdUser());
                    IQueryable<Users> filtredList = unitOfWork.Users.List(u => u.AddressTable.City == "Aalborg");
                    Assert.AreEqual(2, filtredList.ToArray().Count());
                }
                catch
                {
                    Assert.Fail();
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        //Testing an edition of existing user in a database
        [TestMethod]
        public void Edition_Of_User_Is_Saved()
        {
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    Users u = unitOfWork.Users.Create(GetUser());
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

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }
    }
}

