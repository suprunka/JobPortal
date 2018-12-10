using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Linq;
using System.Linq;
using Repository;
using UnitTestProject1.Database_tests;
using AddressTable = Repository.DbConnection.AddressTable;
using Users = Repository.DbConnection.Users;
using AspNetUser = Repository.DbConnection.AspNetUsers;
using Gender = Repository.DbConnection.Gender;

namespace UnitTestProject1
{
    //Works
    [TestClass]
    public class RepositoryTest
    {
        //Creating users for test purpose

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
                AspNetUsers = new AspNetUser
                {
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
                
                AddressTable = new AddressTable
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midjylland"
                },
                AspNetUsers = new AspNetUser
                {
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
                AddressTable = new AddressTable
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midtjylland"
                },
                AspNetUsers = new AspNetUser
                {
                    PasswordHash = "Adama1",
                    UserName = "Username12",
                    PhoneNumber = "07654321",
                    Email = "Updated",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 4,
                },
                Gender = new Gender
                {
                    Gender1 = "Female",
                },
                PayPalMail = "lal@wp.pl",
                FirstName = "Updated",
                LastName = "Updated",
                AddressLine = "Updated",
                Description = "Description",
            };
            return userStub;

        }
        private static Users ForthUser()
        {
            var userStub = new Users
            {

                     AddressTable = new AddressTable
                     {
                         Postcode = "9000",
                         City = "Aalborg",
                         Region = "Nordjylland"
                     },
                AspNetUsers = new AspNetUser
                     {
                         PasswordHash = "Adama1",
                         UserName = "Username11",
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
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                AspNetUsers = new AspNetUser
                {
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



        //Testing adding a valid object to a database. [OK]
        [TestMethod]
        public void Add_TestClassUserObjectPassed()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {

                var result = unitOfWork.Users.Create(GetUser());

                Assert.IsNotNull(result);

                Assert.IsNotNull(context.Users.FirstOrDefault());
                context.Users.DeleteAllOnSubmit(context.Users);
                context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
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

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(GetAnotherUser());
                }
                catch (DuplicateKeyException)
                {
                    int numberOfUsers = context.Users.Count();
                    int numberOfAddresses = context.AddressTable.Count();
                    int numberOfAspNetUsers = context.AspNetUsers.Count();
                    Assert.AreEqual(1, numberOfUsers);
                    Assert.AreEqual(1, numberOfAddresses);
                    Assert.AreEqual(1, numberOfAspNetUsers);
                }
            }

            var secondContext = new DbTestDataContext();
             using (var unitOfWork = new UnitOfWork(secondContext))
             {
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                 secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                 secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                 secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    var result = unitOfWork.Users.Create(GetUser());
                    Assert.IsNotNull(result);
                    unitOfWork.Users.Create(GetUser());
                }
                catch (DuplicateKeyException)
                {

                }
            }

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    var x = unitOfWork.Users.Create(GetUser());
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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    var user = unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(ThirdUser());
                    unitOfWork.Users.Delete(t => t.ID == user.ID);
                    Assert.IsNotNull(context.AddressTable.First());
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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    var user = unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Delete(t => t.ID == user.ID);
                    Assert.AreEqual(0, context.AddressTable.Count());
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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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

            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    unitOfWork.Users.Create(GetUser());
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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(ForthUser());
                    unitOfWork.Users.Create(ThirdUser());
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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
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
                    Users u = unitOfWork.Users.Create(GetUser());
                    Users toUpdate = ToUpdate();
                    toUpdate.ID = u.ID;
                    toUpdate.LastUpdate = u.LastUpdate;

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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Edition_Of_Users_Mail_Returns_User_Is_Successed()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    Users u = unitOfWork.Users.Create(GetUser());
                    Users toUpdate = ToUpdate();
                    toUpdate.ID = u.ID;

                    var result = unitOfWork.Users.UpdateUserMail(toUpdate);
                    Assert.IsNotNull(result);
                    Assert.AreEqual("Updated", result.AspNetUsers.Email);
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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Edition_Of_Users_Description_Returns_User_Is_Successed()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    Users u = unitOfWork.Users.Create(GetUser());
                    Users toUpdate = ToUpdate();
                    toUpdate.ID = u.ID;

                    var result = unitOfWork.Users.AddDescription(toUpdate);
                    Assert.IsNotNull(result);
                    Assert.AreEqual("Description", result.Description);
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
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }
    }
}

