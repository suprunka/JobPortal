using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using ServiceLibrary.Models;

using System.Linq;
using System.Linq.Expressions;
using System;
using Repository;
using JobPortal.Model;
using Repository.DbConnection;
using Gender = JobPortal.Model.Gender;
using System.Data.Linq;

namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class UserServiceTests
    {

        public Mock<User> GetMockOfUser()
        {
            var userStub = new Mock<User>();
            userStub.Setup(x => x.FirstName).Returns("Ådam");
            userStub.Setup(x => x.PhoneNumber).Returns("12345678");
            userStub.Setup(x => x.LastName).Returns("Ådåm");
            userStub.Setup(x => x.Email).Returns("am@gmail.com");
            userStub.Setup(x => x.UserName).Returns("AdamM");
            userStub.Setup(x => x.Password).Returns("Qwerty1");
            userStub.Setup(x => x.AddressLine).Returns("Stræætline");
            userStub.Setup(x => x.CityName).Returns("Cityæname");
            userStub.Setup(x => x.Postcode).Returns("2154");
            userStub.Setup(x => x.Region).Returns(Region.Hovedstaden);
            userStub.Setup(x => x.Gender).Returns(Gender.Male);
            userStub.Setup(x => x.Description).Returns("Description");
            userStub.Setup(x => x.PayPalMail).Returns("Paypal@wp.pl");
            return userStub;
        }

        //CreateUser    
        [TestMethod]
        public void Create_UserService_Creation_Of_User_Hit_Database_Once()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.Create(It.IsAny<Users>()));
                UserService service = new UserService(databaseMock.Object);
                service.CreateUser(GetMockOfUser().Object, null);
                databaseMock.Verify(t => t.Users.Create(It.IsAny<Users>()), Times.Once);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Create_UserService_Creation_Of_User_With_Valid_Inputs_Is_True()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.Create(It.IsAny<Users>()));
                UserService service = new UserService(databaseMock.Object);
                bool result = service.CreateUser(GetMockOfUser().Object, null);
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Create_UserService_Creation_Of_User_Not_Using_Required_Arguments_In_Regex_Throws_ArgumentNullException(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
        {
            try
            {
                var userStub = new Mock<User>();
                #region
                userStub.Setup(x => x.FirstName).Returns(firstName);
                userStub.Setup(x => x.PhoneNumber).Returns(phoneNumber);
                userStub.Setup(x => x.LastName).Returns(lastName);
                userStub.Setup(x => x.Email).Returns(email);
                userStub.Setup(x => x.UserName).Returns(userName);
                userStub.Setup(x => x.Password).Returns(password);
                userStub.Setup(x => x.AddressLine).Returns(addressLine);
                userStub.Setup(x => x.CityName).Returns(cityName);
                userStub.Setup(x => x.Postcode).Returns(postCode);
                userStub.Setup(x => x.Region).Returns(region);
                userStub.Setup(x => x.Gender).Returns(gender);

                #endregion
                var databaseMock = new Mock<IUnitOfWork>();
                UserService service = new UserService(databaseMock.Object);
                service.CreateUser(userStub.Object, null);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }
        #region
        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypąl@wp.pl")] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypalwp.pl")] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wppl")] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid postcode (not allowed characters)
        #endregion
        [TestMethod]
        public void Create_UserService_Creation_Of_User_Using_Different_Invalid_Arguments_Is_False(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender, string description, string paypalMail)
        {
            try
            {
                var userStub = new Mock<User>();
                userStub.Setup(x => x.FirstName).Returns(firstName);
                userStub.Setup(x => x.PhoneNumber).Returns(phoneNumber);
                userStub.Setup(x => x.LastName).Returns(lastName);
                userStub.Setup(x => x.Email).Returns(email);
                userStub.Setup(x => x.UserName).Returns(userName);
                userStub.Setup(x => x.Password).Returns(password);
                userStub.Setup(x => x.AddressLine).Returns(addressLine);
                userStub.Setup(x => x.CityName).Returns(cityName);
                userStub.Setup(x => x.Postcode).Returns(postCode);
                userStub.Setup(x => x.Region).Returns(region);
                userStub.Setup(x => x.Gender).Returns(gender);
                userStub.Setup(x => x.Description).Returns(description);
                userStub.Setup(x => x.PayPalMail).Returns(paypalMail);
                var databaseMock = new Mock<IUnitOfWork>();
                UserService service = new UserService(databaseMock.Object);
                Assert.IsFalse(service.CreateUser(userStub.Object, null));
            }
            catch
            {
                Assert.Fail();
            }
        }

        //FindUser
        [TestMethod]
        public void Get_UserService_Finding_User_By_Valid_ID_Returns_User()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
                {
                    AddressTable = new AddressTable
                    {
                        Postcode = "9000",
                        City = "Aalborg",
                        Region = "Nordjylland"
                    },
                    AspNetUsers = new AspNetUsers
                    {
                        PasswordHash = "Adama1",
                        PhoneNumber = "12345678",
                        Email = "adam@gmail.com",
                        UserName = "Username1",
                    },
                    Gender = new Repository.DbConnection.Gender
                    {
                        Gender1 = "Male",
                    },
                    ID = 1,
                    FirstName = "Adam",
                    LastName = "Adam",
                    AddressLine = "mickiewicza",
                    Description = "description",
                    PayPalMail = "paypal@wp.pl",
                });
                var subject = new UserService(databaseMock.Object);
                var foundUser = subject.FindUserByID(1);
                Assert.IsInstanceOfType(foundUser, typeof(User));
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Get_UserService_Using_Invalid_ID_Returns_Null()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();

               databaseMock.Setup(x => x.Users.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
                {
                    AddressTable = new AddressTable
                    {
                        Postcode = "9000",
                        City = "Aalborg",
                        Region = "Nordjylland"
                    },
                    AspNetUsers = new AspNetUsers
                    {
                        PasswordHash = "Adama1",
                        PhoneNumber = "12345678",
                        Email = "adam@gmail.com",
                        UserName = "Username1",
                    },
                    Gender = new Repository.DbConnection.Gender
                    {
                        Gender1 = "Male",
                    },
                    ID = 1,
                    FirstName = "Adam",
                    LastName = "Adam",
                    AddressLine = "mickiewicza",
                    Description = "description",
                    PayPalMail = "paypal@wp.pl",
                });
                var subject = new UserService(databaseMock.Object);
                var foundUser = subject.FindUserByID(-1);
                Assert.IsNull(foundUser);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Get_UserService_Finding_User_By_Valid_ID_Hits_Database_Once()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
                {
                    AddressTable = new AddressTable
                    {
                        Postcode = "9000",
                        City = "Aalborg",
                        Region = "Nordjylland"
                    },
                    AspNetUsers = new AspNetUsers
                    {
                        PasswordHash = "Adama1",
                        PhoneNumber = "12345678",
                        Email = "adam@gmail.com",
                        UserName = "Username1",
                    },
                    Gender = new Repository.DbConnection.Gender
                    {
                        Gender1 = "Male",
                    },
                    ID = 1,
                    FirstName = "Adam",
                    LastName = "Adam",
                    AddressLine = "mickiewicza",
                    Description = "description",
                    PayPalMail = "paypal@wp.pl",
                });
                var subject = new UserService(databaseMock.Object).FindUserByID(1);
                databaseMock.Verify(x => x.Users.Get(It.IsAny<Expression<Func<Users, bool>>>()), Times.Once());
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Get_UserService_Catch_Exception_If_Database_Throws_It()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.Get(It.IsAny<Expression<Func<Users, bool>>>())).Throws(new Exception());
                var subject = new UserService(databaseMock.Object);
                subject.FindUserByID(1);

            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void Get_UserService_Check_Its_Proporties()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();

                databaseMock.Setup(x => x.Users.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
                {
                    AddressTable = new AddressTable
                    {
                        Postcode = "9000",
                        City = "Aalborg",
                        Region = "Nordjylland"
                    },
                    AspNetUsers = new AspNetUsers
                    {
                        PasswordHash = "Adama1",
                        PhoneNumber = "12345678",
                        Email = "adam@gmail.com",
                        UserName = "Username1",
                    },
                    Gender = new Repository.DbConnection.Gender
                    {
                        Gender1 = "Male",
                    },
                    ID = 1,
                    FirstName = "Adam",
                    LastName = "Adam",
                    AddressLine = "mickiewicza",
                    Description = "description",
                    PayPalMail = "paypal@wp.pl",

                });
                var subject = new UserService(databaseMock.Object);
                var foundUser = subject.FindUserByID(1);
                Assert.AreEqual("description", foundUser.Description);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetAll_UserService_Hits_Database_Once()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.GetAll()).Returns(new Users[] { new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Nordjylland"
                 },
                  AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "Adama1",
                     PhoneNumber = "12345678",
                     Email = "adam@gmail.com",
                     UserName = "Username1",
                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 1,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                     Description = "description",
                    PayPalMail = "paypal@wp.pl",
             },new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Nordjylland"
                 },
                  AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "Adama1",
                     PhoneNumber = "12345678",
                     Email = "adam@gmail.com",
                     UserName = "Username1",
                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 1,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                     Description = "description",
                    PayPalMail = "paypal@wp.pl",

             },new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                 AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "Adama1",
                     PhoneNumber = "12345678",
                     Email = "adam@gmail.com",
                     UserName = "Username1",
                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 1,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                     Description = "description",
                    PayPalMail = "paypal@wp.pl",

             },new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                  AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "Adama1",
                     PhoneNumber = "12345678",
                     Email = "adam@gmail.com",
                     UserName = "Username1",
                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 1,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                     Description = "description",
                    PayPalMail = "paypal@wp.pl",

             }, new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                  AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "adamapA1",
                     UserName = "Username5",
                    PhoneNumber = "87654321",
                     Email = "adam@gmail.com",


                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 5,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                     Description = "description",
                    PayPalMail = "paypal@wp.pl",

             }
             }.AsQueryable());
                var list = new UserService(databaseMock.Object).GetAll();
                databaseMock.Verify(x => x.Users.GetAll(), Times.Once());
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetAll_UserService_Catch_Exception_If_Database_Thrws_It()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.GetAll()).Throws(new Exception());
                new UserService(databaseMock.Object).GetAll();
                
            }
            catch(Exception)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void GetAll_UserService_Verify_If_Returns_Queryable()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.GetAll()).Returns(new Users[] { new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Nordjylland"
                 },
                AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "adamapA1",
                     UserName = "Username5",
                    PhoneNumber = "87654321",
                     Email = "adam@gmail.com",


                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 5,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                   Description = "description",
                        PayPalMail = "paypal@wp.pl",

                     }, new Users {
                         AddressTable = new AddressTable
                         {
                             Postcode = "9000",
                             City = "Aalborg",
                             Region = "Nordjylland"
                         },
                          AspNetUsers = new AspNetUsers
                         {
                             PasswordHash = "adamapA1",
                             UserName = "Username5",
                            PhoneNumber = "87654321",
                             Email = "adam@gmail.com",


                         },
                         Gender = new Repository.DbConnection.Gender
                         {
                             Gender1 = "Male",
                         },
                         ID = 5,
                         FirstName = "Adam",
                         LastName = "Adam",
                         AddressLine = "mickiewicza",
                           Description = "description",
                                PayPalMail = "paypal@wp.pl",

                     }
             }.AsQueryable());
                var list = new UserService(databaseMock.Object).GetAll();
                Assert.AreEqual(2, list.Count());
            }
            catch
            {
                Assert.Fail();
            }
        }

        //ListByRegion
        [TestMethod]
        public void List_By_Region_Test()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.List(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users[] { new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                  AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "adamapA1",
                     UserName = "Username5",
                    PhoneNumber = "87654321",
                     Email = "adam@gmail.com",


                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 5,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                     Description = "description",
                    PayPalMail = "paypal@wp.pl",

                },new Users {
                    AddressTable = new AddressTable
                     {
                         Postcode = "9000",
                         City = "Aalborg",
                         Region = "Midtjylland"
                     },
                    AspNetUsers = new AspNetUsers
                     {
                         PasswordHash = "adamapA1",
                         UserName = "Username5",
                        PhoneNumber = "87654321",
                         Email = "adam@gmail.com",


                     },
                     Gender = new Repository.DbConnection.Gender
                     {
                         Gender1 = "Male",
                     },
                     ID = 5,
                     FirstName = "Adam",
                     LastName = "Adam",
                     AddressLine = "mickiewicza",
                    Description = "description",
                        PayPalMail = "paypal@wp.pl",

             }, new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                  AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "adamapA1",
                     UserName = "Username5",
                    PhoneNumber = "87654321",
                     Email = "adam@gmail.com",


                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 5,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                     Description = "description",
                    PayPalMail = "paypal@wp.pl",

             }
             }.AsQueryable());
                var list = new UserService(databaseMock.Object).ListByRegion(Region.Midtjylland);
                Assert.AreEqual(3, list.Count());
            }
            catch
            {
                Assert.Fail();

            }
        }
        //ListByGender
        [TestMethod]
        public void List_By_Gender_Test()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Users.List(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users[] { new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                  AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "adamapA1",
                     UserName = "Username5",
                    PhoneNumber = "87654321",
                     Email = "adam@gmail.com",


                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 5,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                   Description = "description",
                        PayPalMail = "paypal@wp.pl",

             },new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                 AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "adamapA1",
                     UserName = "Username5",
                    PhoneNumber = "87654321",
                     Email = "adam@gmail.com",


                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 5,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                   Description = "description",
                        PayPalMail = "paypal@wp.pl",
             }, new Users {
                 AddressTable = new AddressTable
                 {
                     Postcode = "9000",
                     City = "Aalborg",
                     Region = "Midtjylland"
                 },
                 AspNetUsers = new AspNetUsers
                 {
                     PasswordHash = "adamapA1",
                     UserName = "Username5",
                    PhoneNumber = "87654321",
                     Email = "adam@gmail.com",


                 },
                 Gender = new Repository.DbConnection.Gender
                 {
                     Gender1 = "Male",
                 },
                 ID = 5,
                 FirstName = "Adam",
                 LastName = "Adam",
                 AddressLine = "mickiewicza",
                   Description = "description",
                        PayPalMail = "paypal@wp.pl",

             }
             }.AsQueryable());
                var list = new UserService(databaseMock.Object).ListByGender(Gender.Male);
                Assert.AreEqual(3, list.Count());
            }
            catch
            {
                Assert.Fail();
            }
        }

        //DeleteUser
        [TestMethod]
        public void Delete_UserService_Delete_User_With_Valid_ID_Returns_True()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.ID).Returns(1);
            var dbMock = new Mock<IUnitOfWork>();
            dbMock.Setup(t => t.Users.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            bool result = sut.DeleteUser(userMock.Object.ID);
            Assert.IsTrue(result);
        }   
        [TestMethod]
        public void Delete_UserService_Delete_User_With_Invalid_ID_Returns_False()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.ID).Returns(-1);
            var dbMock = new Mock<IUnitOfWork>();
            dbMock.Setup(t => t.Users.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            bool result = sut.DeleteUser(userMock.Object.ID);
            Assert.IsFalse(result);
        }       
        [TestMethod]
        public void Delete_UserService_Delete_User_With_Valid_ID_Hits_Database_Once()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.ID).Returns(1);
            var dbMock = new Mock<IUnitOfWork>();
            dbMock.Setup(t => t.Users.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            sut.DeleteUser(userMock.Object.ID);
            dbMock.Verify(x => x.Users.Delete(It.IsAny<Expression<Func<Users, bool>>>()), Times.Once);
        }        
        [TestMethod]
        public void Delete_UserService_Delete_User_With_Invalid_ID_Never_Hits_Database()
        {
            var userMock = new Mock<User>();
            var dbMock = new Mock<IUnitOfWork>();
            dbMock.Setup(t => t.Users.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            sut.DeleteUser(-1);
            dbMock.Verify(x => x.Users.Delete(It.IsAny<Expression<Func<Users, bool>>>()), Times.Never);
        }

        //Update    
        [TestMethod]
        public void Update_UserService_Using_Valid_Parameters_Hit_Database_Once()
        {
            var databaseMock = new Mock<IUnitOfWork>();
            databaseMock.Setup(x => x.Users.Update(It.IsAny<Users>()));
            new UserService(databaseMock.Object).EditUser(GetMockOfUser().Object);
            databaseMock.Verify(x => x.Users.Update(It.IsAny<Users>()), Times.Once());
        }
        [TestMethod]
        public void Update_UserService_Using_Valid_Parameters_Is_True()
        {
            var databaseMock = new Mock<IUnitOfWork>();
            databaseMock.Setup(t => t.Users.Update(It.IsAny<Users>()));
            var result = new UserService(databaseMock.Object).EditUser(GetMockOfUser().Object);
            Assert.IsTrue(result);
        }


        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypąl@wp.pl")] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypalwp.pl")] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wppl")] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid postcode (not allowed characters)#endregion
        [TestMethod]
        public void Update_UserService_Using_Invalid_Parameters_Returns_False(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender, string description, string paypalMail)
        {
            var userStub = new Mock<User>();
            userStub.Setup(x => x.FirstName).Returns(firstName);
            userStub.Setup(x => x.PhoneNumber).Returns(phoneNumber);
            userStub.Setup(x => x.LastName).Returns(lastName);
            userStub.Setup(x => x.Email).Returns(email);
            userStub.Setup(x => x.UserName).Returns(userName);
            userStub.Setup(x => x.Password).Returns(password);
            userStub.Setup(x => x.AddressLine).Returns(addressLine);
            userStub.Setup(x => x.CityName).Returns(cityName);
            userStub.Setup(x => x.Postcode).Returns(postCode);
            userStub.Setup(x => x.Region).Returns(region);
            userStub.Setup(x => x.Gender).Returns(gender);
            userStub.Setup(x => x.Description).Returns(description);
            userStub.Setup(x => x.PayPalMail).Returns(paypalMail);
            var databaseMock = new Mock<IUnitOfWork>();
            var result = new UserService(databaseMock.Object).EditUser(userStub.Object);
            Assert.IsFalse(result);
        }

        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypąl@wp.pl")] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypalwp.pl")] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wppl")] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male, "Description", "Paypal@wp.pl")] //invalid postcode (not allowed characters)
        [TestMethod]
        public void Update_UserService_Using_Invalid_Parameters_Doesnt_Hit_Database(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender, string description, string paypalMail)
        {
            var userStub = new Mock<User>();
            userStub.Setup(x => x.FirstName).Returns(firstName);
            userStub.Setup(x => x.PhoneNumber).Returns(phoneNumber);
            userStub.Setup(x => x.LastName).Returns(lastName);
            userStub.Setup(x => x.Email).Returns(email);
            userStub.Setup(x => x.UserName).Returns(userName);
            userStub.Setup(x => x.Password).Returns(password);
            userStub.Setup(x => x.AddressLine).Returns(addressLine);
            userStub.Setup(x => x.CityName).Returns(cityName);
            userStub.Setup(x => x.Postcode).Returns(postCode);
            userStub.Setup(x => x.Region).Returns(region);
            userStub.Setup(x => x.Gender).Returns(gender);
            userStub.Setup(x => x.Description).Returns(description);
            userStub.Setup(x => x.PayPalMail).Returns(paypalMail);
            var databaseMock = new Mock<IUnitOfWork>();
            var sut = new UserService(databaseMock.Object);
            sut.EditUser(userStub.Object);
            databaseMock.Verify(x => x.Users.Update(It.IsAny<Users>()), Times.Never());

        }
    }
}

