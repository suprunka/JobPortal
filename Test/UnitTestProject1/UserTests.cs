using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using System.Collections.Generic;
using ServiceLibrary.DbConnection;
using System.Linq;
using ServiceLibrary.Models;
using Repositories;
using WebJobPortal;
using WebJobPortal.Controllers;
using WebJobPortal.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tests
{
    [TestClass]
    public class UserTests
    {
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

        
    }
}
