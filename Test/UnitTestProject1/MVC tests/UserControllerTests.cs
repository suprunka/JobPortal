using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using ServiceLibrary.Models;
using WebJobPortal.Controllers;
using System.Web.Mvc;
using System.Linq;

namespace UnitTestProject1.MVC_tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestClass]
        public class MovieControllerTests
        {
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
}
