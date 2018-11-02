using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary;
using ServiceLibrary.Models;
using Repositories;
using System.Linq;
using System.Linq.Expressions;
using System;

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
        public void Get_UserService_Verify_If_It_Calls_Db()
        {

            var dbMock = new Mock<IRepository<User>>();

            var sut = new UserService(dbMock.Object);
            var foundUser = sut.FindUser(1);
            dbMock.Verify(x => x.Get(It.IsAny<Expression<Func<User,bool>>>()), Times.Once());
            Assert.AreEqual(1, foundUser.ID);
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

    }
}
