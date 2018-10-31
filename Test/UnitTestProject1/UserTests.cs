using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using System.Collections.Generic;
using ServiceLibrary.DbConnection;
using System.Linq;
using ServiceLibrary.Models;
using Repositories;

namespace Tests
{
    [TestClass]
    public class UserTests
    {


        
        [TestMethod]
        public void Test_Service_Creation_Of_User_With_Different_Data()
        {
            
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
        public void Read_User_Return_Valid_User()
        {

            var userMock = new Mock<User>();
            userMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IRepository<User>>();
            int userId = -1;

            dbMock.Setup(x => x.Get(It.IsAny<int>()))
                .Callback<int>(x => userId = x);

            var sut = new UserService(dbMock.Object);
            sut.FindUser(userMock.Object.Id);
            // var aa = sut.FindUser(1);
            // Assert.AreEqual(1, aa.Id);

            Assert.AreEqual(1, userId);
        }
        [TestMethod]
        public void Read_User_Hits_Db_Once()
        {

            var userMock = new Mock<User>();
            userMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IRepository<User>>();
           
            var sut = new UserService(dbMock.Object);
            sut.FindUser(userMock.Object.Id);
          
            dbMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
        }


        [TestMethod]
        public void Test_Service_Update_Of_User()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.LastName).Returns("Adam");

        }
    }
}
