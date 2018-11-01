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
                return new List<User> { new User() { Id = 1 }, new User() { Id = 71 }, new User() { Id = 10 } };
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
        public void Edit_Will_Show_View_Of_Updated_User()
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


    }
}
