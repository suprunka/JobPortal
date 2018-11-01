using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using ServiceLibrary.Models;
using Repositories;
using WebJobPortal;
using WebJobPortal.Controllers;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UserTests
    {
        [DataRow("123456789", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (too many characters)
        [DataRow("12345œæ", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (not allowed characters)
        [DataRow("12345678", "Adaœ", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam¿œæ", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaœ@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaœgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaœ@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana¿", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid userName (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "he", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //too short userName
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password no capital letter
        [DataRow("12345678", "Adam", "Adam", "Adaœ@gmail.com", "AdamMana", "Qwerty", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password without number
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline¿æ", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamê", "2154", Region.Hovedstaden, Gender.Male)] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male)] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "œæê¿", Region.Hovedstaden, Gender.Male)] //invalid postcode (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //valid all data
        [TestMethod]
        public void Test_UserWebModel_validation(string phoneNumber, string firstName,
            string lastName, string email, string userName, string password, string addressLine,
            string cityName, string postCode, Region region, Gender gender)
        {
            var userServiceStub = new UserWebModel{ 
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password,
                AddressLine = addressLine,
                CityName = cityName,
                Postcode = postCode,
                Region = region,
                Gender = gender
            };
            
            var sub = new UserController(userServiceStub.Object);

        }


        [TestMethod]
        public void Test_Service_Creation_Of_User_Hit_Database_Once()
        {
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
            var databaseMock = new Mock<IRepository<User>>();
            UserService service = new UserService(databaseMock.Object);
            service.CreateUser(userMock.Object);
            databaseMock.Verify(t => t.Create(It.IsAny<User>()), Times.AtLeastOnce);
        }


        [TestMethod]
        public void Test_Service_Creation_Of_User_Is_Not_Null()
        {
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
            var databaseMock = new Mock<IRepository<User>>();
            UserService service = new UserService(databaseMock.Object);
            User u = service.CreateUser(userMock.Object);
            Assert.IsNotNull(u);
        }

        [TestMethod]
        public void Test_Service_Delete_Of_User_Hit_Database_Once()
        {
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
            var databaseMock = new Mock<IRepository<User>>();
            UserService service = new UserService(databaseMock.Object);
            bool result = service.DeleteUser(userMock.Object.PhoneNumber);
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Test_Service_Delete_Of_User()
        {
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
            var databaseMock = new Mock<IRepository<User>>();
            UserService service = new UserService(databaseMock.Object);
            bool result = service.DeleteUser("45205657");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Get_UserService_Verify_If_It_Calls_Db()
        {
            
            var dbMock = new Mock<IRepository<User>>();
        
            var sut = new UserService(dbMock.Object);
            var foundUser =sut.FindUser("1");
            dbMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(1, foundUser.Id);
        }
        [TestMethod]
        public void GetAll_OfferService_Verify_If_Returns_Queryable()
        {
            var dbMock = new Mock<IRepository<User>>();
            IQueryable<User> list = null;
            dbMock.Setup(x => x.GetAll()).Returns(new User[] { new User() }.AsQueryable<User>())
                .Callback<IQueryable<User>>(x => list = x);
            var sut = new UserService(dbMock.Object);
            sut.GetAll();
            dbMock.Verify(x => x.GetAll(), Times.Once());

            Assert.AreEqual(1, list.Count());
        }

        [TestMethod]
        public void Update_UserService_Verify_If_Connect_To_Db()
        {
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
            var dbMock = new Mock<IRepository<User>>();
            var sut = new UserService(dbMock.Object);
            sut.EditUser(userMock.Object);
            dbMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once());

        }

        [TestMethod]
        public void Update_OfferService_Verify_If_Returns_Valid_Object()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.LastName).Returns("Poulsen");
            userMock.Setup(x => x.PhoneNumber).Returns("80901099");
            var dbMock = new Mock<IRepository<User>>();
            User returnedUser = null;
            dbMock.Setup(x => x.Update(It.IsAny<User>()))
                .Callback<User>(x => returnedUser = x);
            var sut = new UserService(dbMock.Object);
            sut.EditUser(userMock.Object);
            Assert.IsTrue(
                returnedUser.LastName.Equals("Poulsen") &&
                returnedUser.PhoneNumber.Equals("80901099")
                 );
        }

        [TestMethod]
        public void Test_Service_Update_Of_User()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.LastName).Returns("Adam");

        }
    }
}
