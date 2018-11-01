using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.App_test
{
    [TestClass]
    public class UserControllerTests
    {
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
