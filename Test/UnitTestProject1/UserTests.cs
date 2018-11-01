using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using ServiceLibrary.Models;
using Repositories;
using WebJobPortal;
using WebJobPortal.Controllers;
using System.Linq;
using WebJobPortal.Models;
using WebJobPortal.Mapping;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UserTests
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            MappingConfig.RegisterMaps();
        }

        [TestMethod]
        public void Test_Create_View()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);

        }

        [TestMethod]
        public void Test_Create_View_Passing_A_Valid_Object()
        {

        }

        [TestMethod]
        public void Test_Create_View_Passing_Invalid_Object()
        {

        }


        [TestMethod]
        public void Test_Create_View_Exception_Expected()
        {

        }


        [DataRow("123456789", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid phonenumber (too many characters)
        [DataRow("12345ść", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid phonenumber (not allowed characters)
        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamManaż", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid userName (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "he", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //too short userName
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid password no capital letter
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid password without number
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male, false)] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male, false)] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male, false)] //invalid postcode (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, true)] //valid all data
        [TestMethod]
        public void Test_UserWebModel_validation(string phoneNumber, string firstName,
          string lastName, string email, string userName, string password, string addressLine,
          string cityName, string postCode, Region region, Gender gender, bool shouldValidate)
        {
            var userServiceStub = new UserWebModel
            {
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

            var context = new ValidationContext(userServiceStub, null, null);
            var result = new List<ValidationResult>();


            var isModelStateValid = Validator.TryValidateObject(userServiceStub, context, result, true);

            Assert.IsTrue(isModelStateValid == shouldValidate);
        }


        [TestMethod]
        public void Test_MVCController_Can_Create_User_With_Valid_Inputs()
        {
            var userStub = new Mock<UserWebModel>().SetupAllProperties();
            userStub.Setup(x => x.FirstName).Returns("Adam");
            userStub.Setup(x => x.PhoneNumber).Returns("12345678");
            userStub.Setup(x => x.LastName).Returns("Adam");
            userStub.Setup(x => x.Email).Returns("adam@gmail.com");
            userStub.Setup(x => x.UserName).Returns("Adammana");
            userStub.Setup(x => x.Password).Returns("Adama1");
            userStub.Setup(x => x.AddressLine).Returns("Adaminsæøa");
            userStub.Setup(x => x.CityName).Returns("Ålborg");
            userStub.Setup(x => x.Postcode).Returns("9000");
            userStub.Setup(x => x.Region).Returns(Region.Nordjylland);
            userStub.Setup(x => x.Gender).Returns(Gender.Male);

            User u = null;
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Callback<User>(x => u = x);

            var subject = new UserController(serviceMock.Object);

            subject.Create(userStub.Object);

            Assert.IsTrue(u.FirstName == "Adam" &&
                u.Gender == Gender.Male &&
                u.PhoneNumber == "12345678");
        }

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
        [TestMethod]
        public void Test_MVCController_Will_Not_Create_A_Movie_With_Invalid_Model_State(string phoneNumber, string firstName,
         string lastName, string email, string userName, string password, string addressLine,
         string cityName, string postCode, Region region, Gender gender)
        {
            var serviceMock = new Mock<IUserService>();
            var userStub = new Mock<UserWebModel>().SetupAllProperties();
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
            var subject = new UserController(serviceMock.Object);
            subject.ModelState.AddModelError("RegularExpression", "Doesn't match regex");
            ViewResult resultPage = subject.Create(userStub.Object) as ViewResult;
            Assert.IsTrue("Create" == resultPage.ViewName);
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
            var foundUser = sut.FindUser("1");
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
    }
}

