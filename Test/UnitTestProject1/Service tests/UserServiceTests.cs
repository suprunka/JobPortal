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
        //Create
        #region

        //Test if during creation of user database is hit only once. 
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Create_UserService_Creation_Of_User_Hit_Database_Once(string phoneNumber, string firstName,
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
                var databaseMock = new Mock<IUserRepository>();
                UserService service = new UserService(databaseMock.Object);
                service.CreateUser(userStub.Object);
                databaseMock.Verify(t => t.Create(It.IsAny<Users>()), Times.AtLeastOnce);
            }
            catch
            {
                Assert.Fail();
            }
        }


        //Test if creation of user results sucessfully.
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Create_UserService_Creation_Of_User_With_Valid_Inputs_Is_True(string phoneNumber, string firstName,
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
                var databaseMock = new Mock<IUserRepository>();
                UserService service = new UserService(databaseMock.Object);
                bool result = service.CreateUser(userStub.Object);
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }


        //Test creation users with valid proporties which should pass. 
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Create_UserService_Creation_Of_User_Using_Valid_Arguments_Is_Not_Null(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
        {
            try
            {
                var userStub = new Mock<User>().SetupAllProperties();
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
                var databaseMock = new Mock<IUserRepository>();
                UserService service = new UserService(databaseMock.Object);
                Assert.IsNotNull(service.CreateUser(userStub.Object));
            }
            catch
            {
                Assert.Fail();
            }
        }


        //Test creation users with invalid proporties which shouldn't pass.
        #region
        [DataRow("123456789", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (too many characters)
        [DataRow("12345ść", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (not allowed characters)
        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamManaż", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid userName (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "he", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //too short userName
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password no capital letter
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password without number
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male)] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male)] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male)] //invalid postcode (not allowed characters)
        #endregion
        [TestMethod]
        public void Create_UserService_Creation_Of_User_Using_Different_Invalid_Arguments_Is_False(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
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
                var databaseMock = new Mock<IUserRepository>();
                UserService service = new UserService(databaseMock.Object);
                Assert.IsFalse(service.CreateUser(userStub.Object));
            }
            catch
            {
                Assert.Fail();
            }
        }

        #endregion

        //Read
        #region
        [TestMethod]
        public void Get_UserService_Finding_User_By_Valid_ID_Returns_User()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
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
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            });
            var subject = new UserService(databaseMock.Object);
            var foundUser = subject.FindUserByID(1);
            Assert.IsInstanceOfType(foundUser, typeof(User));
        }

        [TestMethod]
        public void Get_UserService_Using_Invalid_ID_Returns_Null()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
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
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            });
            var subject = new UserService(databaseMock.Object);
            var foundUser = subject.FindUserByID(-1);
            Assert.IsNull(foundUser);
        }

        [TestMethod]
        public void Get_UserService_Finding_User_By_Valid_ID_Hits_Database_Once()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
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
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            });
            var subject = new UserService(databaseMock.Object).FindUserByID(1) ;
            databaseMock.Verify(x => x.Get(It.IsAny<Expression<Func<Users, bool>>>()), Times.Once());
        }

       
        [TestMethod]
        public void Get_UserService_Verify_If_It_Calls_Db()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
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
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            });
            var subject = new UserService(databaseMock.Object);
            var foundUser = subject.FindUser("12345678");
            databaseMock.Verify(x => x.Get(It.IsAny<Expression<Func<Users, bool>>>()), Times.Once());

        }

        [TestMethod]
        public void Get_UserService_Check_Its_Proporties()
        {
            var databaseMock = new Mock<IUserRepository>();

            databaseMock.Setup(x => x.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users
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
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            });
            var subject = new UserService(databaseMock.Object);
            var foundUser = subject.FindUser("12345678");
            Assert.AreEqual(1, foundUser.ID);

        }

        [TestMethod]
        public void GetAll_UserService_Hits_Database_Once()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.GetAll()).Returns(new Users[] { new Users {
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
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            },new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username2",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 2,
                PhoneNumber = "76584321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            },new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username3",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 3,
                PhoneNumber = "76854321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            },new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username4",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 4,
                PhoneNumber = "78654321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }, new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "adamapA1",
                    UserName = "Username5",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 5,
                PhoneNumber = "87654321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }
            }.AsQueryable());
            var list = new UserService(databaseMock.Object).GetAll();
            databaseMock.Verify(x => x.GetAll(), Times.Once());
        }

        [TestMethod]
        public void List_By_Region_Test()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.List(It.IsAny<Expression<Func<Users,bool>>>())).Returns(new Users[] { new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username3",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 3,
                PhoneNumber = "76854321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            },new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username4",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 4,
                PhoneNumber = "78654321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }, new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "adamapA1",
                    UserName = "Username5",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 5,
                PhoneNumber = "87654321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }
            }.AsQueryable());
            var list = new UserService(databaseMock.Object).ListByRegion(Region.Midtjylland);
            Assert.AreEqual(3, list.Count());
        }

        [TestMethod]
        public void List_By_Gender_Test()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.List(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users[] { new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username3",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 3,
                PhoneNumber = "76854321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            },new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username4",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 4,
                PhoneNumber = "78654321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }, new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Midtjylland"
                },
                Logging = new Logging
                {
                    Password = "adamapA1",
                    UserName = "Username5",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 5,
                PhoneNumber = "87654321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }
            }.AsQueryable());
            var list = new UserService(databaseMock.Object).ListByGender(Gender.Male);
            Assert.AreEqual(3, list.Count());
        }

        [TestMethod]
        public void GetAll_UserService_Verify_If_Returns_Queryable()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.GetAll()).Returns(new Users[] { new Users {
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
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }, new Users {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                Logging = new Logging
                {
                    Password = "adamapA1",
                    UserName = "werok",
                },
                Gender = new Repository.DbConnection.Gender
                {
                    Gender1 = "Male",
                },
                ID = 1,
                PhoneNumber = "87654321",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            }
            }.AsQueryable());
            var list = new UserService(databaseMock.Object).GetAll();



            Assert.AreEqual(2, list.Count());
        }

        #endregion

        //Delete
        #region
        [TestMethod]
        public void Delete_UserService_Delete_User_With_Valid_ID_Returns_True()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.ID).Returns(1);
            var dbMock = new Mock<IUserRepository>();
            dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            bool result = sut.DeleteUser(userMock.Object.ID);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete_UserService_Delete_User_With_Invalid_ID_Returns_False()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.ID).Returns(-1);
            var dbMock = new Mock<IUserRepository>();
            dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            bool result = sut.DeleteUser(userMock.Object.ID);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Delete_UserService_Delete_User_With_Valid_ID_Hits_Database_Once()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.ID).Returns(1);
            var dbMock = new Mock<IUserRepository>();
            dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            sut.DeleteUser(userMock.Object.ID);
            dbMock.Verify(x => x.Delete(It.IsAny<Expression<Func<Users, bool>>>()), Times.Once);
        }

        [TestMethod]
        public void Delete_UserService_Delete_User_With_Invalid_ID_Never_Hits_Database()
        {
            var userMock = new Mock<User>();
            var dbMock = new Mock<IUserRepository>();
            dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<Users, bool>>>())).Returns(true);
            var sut = new UserService(dbMock.Object);
            sut.DeleteUser(-1);
            dbMock.Verify(x => x.Delete(It.IsAny<Expression<Func<Users, bool>>>()), Times.Never);
        }
        #endregion


        //Update
        #region
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Update_UserService_Using_Valid_Parameters_Hit_Database_Once(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.FirstName).Returns(firstName);
            userMock.Setup(x => x.PhoneNumber).Returns(phoneNumber);
            userMock.Setup(x => x.LastName).Returns(lastName);
            userMock.Setup(x => x.Email).Returns(email);
            userMock.Setup(x => x.UserName).Returns(userName);
            userMock.Setup(x => x.Password).Returns(password);
            userMock.Setup(x => x.AddressLine).Returns(addressLine);
            userMock.Setup(x => x.CityName).Returns(cityName);
            userMock.Setup(x => x.Postcode).Returns(postCode);
            userMock.Setup(x => x.Region).Returns(region);
            userMock.Setup(x => x.Gender).Returns(gender);
            var databaseMock = new Mock<IUserRepository>();
            new UserService(databaseMock.Object).EditUser(userMock.Object);
            databaseMock.Verify(x => x.Update(It.IsAny<Users>()), Times.Once());
        }

        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Update_UserService_Using_Valid_Parameters_Is_True(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.FirstName).Returns(firstName);
            userMock.Setup(x => x.PhoneNumber).Returns(phoneNumber);
            userMock.Setup(x => x.LastName).Returns(lastName);
            userMock.Setup(x => x.Email).Returns(email);
            userMock.Setup(x => x.UserName).Returns(userName);
            userMock.Setup(x => x.Password).Returns(password);
            userMock.Setup(x => x.AddressLine).Returns(addressLine);
            userMock.Setup(x => x.CityName).Returns(cityName);
            userMock.Setup(x => x.Postcode).Returns(postCode);
            userMock.Setup(x => x.Region).Returns(region);
            userMock.Setup(x => x.Gender).Returns(gender);
            var databaseMock = new Mock<IUserRepository>();
            var result = new UserService(databaseMock.Object).EditUser(userMock.Object);
            Assert.IsTrue(result);
        }

        #region
        [DataRow("123456789", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (too many characters)
        [DataRow("12345ść", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (not allowed characters)
        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamManaż", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid userName (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "he", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //too short userName
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password no capital letter
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password without number
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male)] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male)] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male)] //invalid postcode (not allowed characters)
        #endregion
        [TestMethod]
        public void Update_UserService_Using_Invalid_Parameters_Returns_False(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.FirstName).Returns(firstName);
            userMock.Setup(x => x.PhoneNumber).Returns(phoneNumber);
            userMock.Setup(x => x.LastName).Returns(lastName);
            userMock.Setup(x => x.Email).Returns(email);
            userMock.Setup(x => x.UserName).Returns(userName);
            userMock.Setup(x => x.Password).Returns(password);
            userMock.Setup(x => x.AddressLine).Returns(addressLine);
            userMock.Setup(x => x.CityName).Returns(cityName);
            userMock.Setup(x => x.Postcode).Returns(postCode);
            userMock.Setup(x => x.Region).Returns(region);
            userMock.Setup(x => x.Gender).Returns(gender);
            var databaseMock = new Mock<IUserRepository>();
            var result = new UserService(databaseMock.Object).EditUser(userMock.Object);
            Assert.IsFalse(result);
        }

        #region
        [DataRow("123456789", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (too many characters)
        [DataRow("12345ść", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (not allowed characters)
        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamManaż", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid userName (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "he", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //too short userName
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password no capital letter
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password without number
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male)] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male)] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male)] //invalid postcode (not allowed characters)
        #endregion
        [TestMethod]
        public void Update_UserService_Using_Invalid_Parameters_Doesnt_Hit_Database(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.FirstName).Returns(firstName);
            userMock.Setup(x => x.PhoneNumber).Returns(phoneNumber);
            userMock.Setup(x => x.LastName).Returns(lastName);
            userMock.Setup(x => x.Email).Returns(email);
            userMock.Setup(x => x.UserName).Returns(userName);
            userMock.Setup(x => x.Password).Returns(password);
            userMock.Setup(x => x.AddressLine).Returns(addressLine);
            userMock.Setup(x => x.CityName).Returns(cityName);
            userMock.Setup(x => x.Postcode).Returns(postCode);
            userMock.Setup(x => x.Region).Returns(region);
            userMock.Setup(x => x.Gender).Returns(gender);
            var databaseMock = new Mock<IUserRepository>();
            var sut = new UserService(databaseMock.Object);
            sut.EditUser(userMock.Object);
            databaseMock.Verify(x => x.Update(It.IsAny<Users>()), Times.Never());

        }
        #endregion
    }
}
