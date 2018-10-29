using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ModelClasses;
using ServiceLibrary;
using System.Collections.Generic;
using ServiceLibrary.DbConnection;
using System.Linq;
using ServiceLibrary.Repository;

namespace Tests
{
    [TestClass]
    public class UserTests
    {
        public UserTests()
        {
            IList<Users> users = new List<Users>
            {
                new Users {PhoneNumber= "85858585", FirstName= "Adam", LastName="Pato³a", Email="ap@wp.pl"},
                new Users {PhoneNumber= "12121212", FirstName= "Wojtek", LastName="Pieczara", Email="wp@wp.pl"}
            };





        }

        [TestMethod]
        public void Test_Service_Creation_Of_User()
        {
            
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();


            var databaseMock = new Mock<IUserRepository>();

            UserService service = new UserService(databaseMock.Object);
            
        
            service.CreateUser(userMock.Object);
            databaseMock.Verify(t => t.CreateUser(It.IsAny<User>()), Times.AtLeastOnce);



        }


        /*[TestMethod]
        public void Test_Service_Delete_Of_User()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.PhoneNumber).Returns("1");
            var servicemoc = new Mock<IUserService>();
            servicemoc.Setup(x => x.CreateUser(new User { PhoneNumber = "1" })).Returns(new User { PhoneNumber = "1" });

            var subject = new UserService();
            Assert.IsTrue(servicemoc.Object.DeleteUser(userMock.Object.PhoneNumber));
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
            userMock.Setup(x => x.LastName).Returns("Adam");

        }*/
    }
}
