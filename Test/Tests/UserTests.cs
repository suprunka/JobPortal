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
        public void TestMethod1()
        {
            var userMock = new Mock<User>();
            var subject = new UserService();
            Assert.IsNotNull(subject.CreateUser(userMock.Object));
        }
    }
}
