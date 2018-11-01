using System;
using AppJobPortal.UserServiceReference;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AppJobPortal.Controllers;
using AppJobPortal.Models;

using User = AppJobPortal.UserServiceReference.User;
using Region = AppJobPortal.UserServiceReference.Region;
using Gender = AppJobPortal.UserServiceReference.Gender;

namespace UnitTestProject1.App_test
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Get_Will_Return_Valid_Object()
        {
            var userMock = new Mock<User>();
            userMock.Setup(x => x.ID == 6);
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.FindUser(It.IsAny<int>())).Returns(userMock.Object);

            var sut = new UserController(userServiceStub.Object);
            UserModel user = sut.Get(6);
            Assert.IsTrue(
                userMock.Object.ID == user.ID
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
        public void Update_OfferService_Verify_If_Returns_Valid_Object(string phoneNumber, string firstName,
         string lastName, string email, string userName, string password, string addressLine,
         string cityName, string postCode, Region region, Gender gender, bool shouldValidate)
        {
            var serviceMock = new Mock<IUserService>();
            var userStub = new Mock<UserModel>().SetupAllProperties();
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
            var usermodel = subject.Edit(userStub.Object);
            bool result = false;
            if (usermodel != null)
                result = true;
            Assert.Equals(result, shouldValidate);
        }

        [TestMethod]
        public void Get_Will_Return_Proper_Amount_Of_Users()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetAll()).Returns(() =>
            {
                return new User[] { new User() { ID=  1 }, new User() { ID = 71 }, new User() { ID = 10 } };
            });

            var sut = new UserController(userServiceStub.Object);
            var result = sut.GetAll();
            Assert.AreEqual( result.Count, 3);
        }
    }
}
