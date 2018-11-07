using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using ServiceLibrary.Models;
using Repositories;
using System.Linq;
using System.Linq.Expressions;
using System;
using Repository;
using JobPortal.Model;
using Repository.DbConnection;
using Gender = JobPortal.Model.Gender;

namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class UserServiceTests
    {

        //Test if during creation of user database is hit only once. [OK]
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Test_Service_Creation_Of_User_Hit_Database_Once(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
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



        //Test if creation of user results sucessfully [OK]
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Test_Service_Creation_Of_User_Is_True(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
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

        //TODO!!
        [DataRow("12345678")]
        [TestMethod]
        public void Test_Finding_A_User_In_Database(string phoneNumber)
        {
           
            var databaseMock = new Mock<IUserRepository>();



            databaseMock.Setup(t => t.Get(It.IsAny<Expression<Func<Users, bool>>>())).Returns(new Users{  FirstName = "Ådam"});
            UserService service = new UserService(databaseMock.Object);
            var u = service.FindUser(phoneNumber);
            Assert.AreEqual("Ådam", u.FirstName);
        }


        //Test creation users with valid proporties which should pass. [OK]
        [DataRow("12345678", "Ådam", "Ådåm", "Ådåm@gmail.com", "AdamMånå", "Qwerty1", "Stræætline", "Cityæname", "2154", Region.Hovedstaden, Gender.Male)]
        [TestMethod]
        public void Test_Creation_Of_User_Using_Valid_Arguments(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
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


        //Test creation users with invalid proporties which shouldn't pass. [OK]
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
        public void Test_Creation_Of_User_Using_Different_Invalid_Arguments(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender)
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

        [TestMethod]
        public void Get_UserService_Verify_If_It_Calls_Db()
        {
            var databaseMock = new Mock<IUserRepository>();
            databaseMock.Setup(x => x.Create(new Users { ID = 1 }));
            databaseMock.Setup(x => x.Get(t => t.ID == It.IsAny<int>())).Returns(() => new Users { ID = 1, PhoneNumber = "12345678" });
            var subject = new UserService(databaseMock.Object);
            var foundUser = subject.FindUser("12345678");
            databaseMock.Verify(x => x.Get(It.IsAny<Expression<Func<Users,bool>>>()), Times.Once());
            Assert.AreEqual(1, foundUser.ID);
        }

        [TestMethod]
        public void GetAll_OfferService_Verify_If_Returns_Queryable()
        {
            var databaseMock = new Mock<IUserRepository>();
            IQueryable<Users> list = null;
            databaseMock.Setup(x => x.GetAll()).Returns(new Users[] { new Users() }.AsQueryable<Users>())
                .Callback<IQueryable<Users>>(x => list = x);
            var sut = new UserService(databaseMock.Object);
            sut.GetAll();
            databaseMock.Verify(x => x.GetAll(), Times.Once());

            Assert.AreEqual(1, list.Count());
        }

        [TestMethod]
        public void Update_UserService_Verify_If_Connect_To_Db()
        {
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
            var databaseMock = new Mock<IUserRepository>();
            var sut = new UserService(databaseMock.Object);
            sut.EditUser(userMock.Object);
            databaseMock.Verify(x => x.Update(It.IsAny<Users>()), Times.Once());

        }

    }
}
