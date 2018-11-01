using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using ServiceLibrary.Models;
using Repositories;
using System;
using System.Collections.Generic;
using WebJobPortal.Controllers;
using System.Web.Mvc;
using System.Linq;
using WebJobPortal.Models;

namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class UserServiceTests
    {
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
            var userMock = new Mock<User>().SetupAllProperties();
            var databaseMock = new Mock<IRepository<User>>();
            UserService service = new UserService(databaseMock.Object);
            User u = service.CreateUser(userMock.Object);
            Assert.IsNotNull(u);
        }

        [TestMethod]
        public void Test_Equality_Of_Proporties_Of_Object_After_Service_Create_It()
        {
            var userMock = new Mock<User>();
            userMock.Object.AddressLine = "Vesterbro";
            var databaseMock = new Mock<IRepository<User>>();
            UserService service = new UserService(databaseMock.Object);
            User createdUser = service.CreateUser(userMock.Object);
            Assert.AreEqual(userMock.Object.AddressLine, createdUser.AddressLine);
        }

        [TestMethod]
        [DataRow("1", 1, DisplayName = "Valid ID ")]
        [DataRow("", 3, DisplayName = "Empty id ")]

        public void Index_Will_return_the_correct_no_of_users_on_search(string Id, int expectedNoOfResults)
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<User> { new User() { ID = 1 }, new User() { ID = 71 }, new User() { ID = 10 } };
            });

            var sut = new UserController(userServiceStub.Object);
            ViewResult resultPage = sut.Index(Id) as ViewResult;

            var model = resultPage.ViewData.Model as IEnumerable<User>;
            Assert.IsTrue(model.Count() == expectedNoOfResults);
        }
        [TestMethod]
        public void Index_Will_show_all_movies_from_service()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<User> { new User(), new User(), new User() };
            });
            var sut = new UserController(userServiceStub.Object);

            var resPage = sut.Index(null) as ViewResult;

            var model = resPage.ViewData.Model as IEnumerable<User>;

            Assert.IsTrue(model.Count() == 3);
        }
        [TestMethod]
        public void Edit_With_Valid_inputs()
        {
            var userMock = new Mock<UserWebModel>();
            userMock.Setup(x => x.LastName).Returns("Dreuk");
            userMock.Setup(x => x.PhoneNumber).Returns("12334455");
            var userServiceStub = new Mock<IUserService>();
            User userSenftoService = null;

            userServiceStub.Setup(x => x.EditUser(It.IsAny<User>()))
                .Callback<User>(x => userSenftoService = x);

            var sut = new UserController(userServiceStub.Object);
            sut.Edit(userMock.Object);
            userServiceStub.Verify(x =>
           x.EditUser(It.IsAny<User>()), Times.Once());

            Assert.IsTrue(
                userSenftoService.LastName == "Dreuk" &&
                userSenftoService.PhoneNumber == "12334455"
                );
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
        public void Update_UserService_Update_Valid_Inputs(string phoneNumber, string firstName,
         string lastName, string email, string userName, string password, string addressLine,
         string cityName, string postCode, Region region, Gender gender, bool shouldValidate)
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
            ViewResult resultPage = subject.Edit(userStub.Object) as ViewResult;
            Assert.IsTrue("Edit" == resultPage.ViewName);
        }
        


    }
}
