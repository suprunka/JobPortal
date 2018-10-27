using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ModelClasses;
using ServiceLibrary;

namespace Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Test_Service_Creation_Of_User()
        {
            var userMock = new Mock<User>();
            var subject = new UserService();
            Assert.IsNotNull(subject.CreateUser(userMock.Object));
        }

        [TestMethod]
        public void Test_Service_Delete_Of_User()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.PhoneNumber).Returns("68457854");
            var subject = new UserService();
            Assert.IsTrue(subject.DeleteUser(userMock.Object.PhoneNumber));
        }

        [TestMethod]
        public void Test_Service_Read_Of_User()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.PhoneNumber).Returns("68457854");
            var subject = new UserService();
            Assert.IsNotNull(subject.FindUser(userMock.Object.PhoneNumber));
        }

        [TestMethod]
        public void Test_Service_Update_Of_User()
        {
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
        }
    }
}
