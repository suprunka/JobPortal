using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebJobPortal.Controllers;
using System.Web.Mvc;
using WebJobPortal.UserServiceReference;
using JobPortal.Model;
using WebJobPortal.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Web;
using TypeMock.ArrangeActAssert;

namespace UnitTestProject1.MVC__tests
{
    [TestClass]
    public class UserTests
    {
        //Create
        #region

        [TestMethod]
        public void Test_Create_View()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                serviceMock.Setup(s => s.CreateUser(It.IsAny<User>())).Returns(true);
                var controller = new UserController(serviceMock.Object);
                var result = controller.Create() as ViewResult;
                Assert.IsTrue("Create" == result.ViewName);
            }
            catch
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void Test_Create_View_Passing_A_Valid_Object()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var controller = new UserController(serviceMock.Object);
                var result = controller.Create(new UserModel
                {
                    PhoneNumber = "12345678",
                    FirstName = "Adam",
                    LastName = "Adam",
                    Email = "Adam@gmail.com",
                    UserName = "AdamMana",
                    Password = "Qwerty1",
                    AddressLine = "Streetline",
                    CityName = "Cityname",
                    Postcode = "2154",
                    Region = Region.Nordjylland,
                    Gender = Gender.Male,
                }) as ActionResult;

                Assert.IsNotNull(result);
            }
            catch
            {
                Assert.Fail();
            }
        }



        [TestMethod]
        public void Test_Create_View_Exception_Expected()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Throws(new Exception());            var controller = new UserController(serviceMock.Object);
            var result = controller.Create(new UserModel
            {
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "Adam@gmail.com",
                UserName = "AdamMana",
                Password = "Qwerty1",
                AddressLine = "Streetline",
                CityName = "Cityname",
                Postcode = "2154",
                Region = Region.Nordjylland,
                Gender = Gender.Male,
            }) as ViewResult;
            Assert.IsTrue("Create" == result.ViewName);
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
        public void Test_UserWebModel_validation(string phoneNumber, string firstName,
          string lastName, string email, string userName, string password, string addressLine,
          string cityName, string postCode, Region region, Gender gender, bool shouldValidate)
        {
            try
            {
                var userServiceStub = new UserModel
                {
                    PhoneNumber = phoneNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = userName,
                    Password = password,
                    AddressLine = addressLine,
                    CityName = cityName,
                    Postcode = postCode,
                    Region = region,
                    Gender = gender
                };

                var context = new ValidationContext(userServiceStub, null, null);
                var result = new List<ValidationResult>();


                var isModelStateValid = Validator.TryValidateObject(userServiceStub, context, result, true);

                Assert.IsTrue(isModelStateValid == shouldValidate);
            }
            catch
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void Test_MVCController_Can_Create_User_With_Valid_Inputs()
        {
            try
            {
                var userStub = new Mock<UserModel>().SetupAllProperties();
                userStub.Setup(x => x.FirstName).Returns("Adam");
                userStub.Setup(x => x.PhoneNumber).Returns("12345678");
                userStub.Setup(x => x.LastName).Returns("Adam");
                userStub.Setup(x => x.Email).Returns("adam@gmail.com");
                userStub.Setup(x => x.UserName).Returns("Adammana");
                userStub.Setup(x => x.Password).Returns("Adama1");
                userStub.Setup(x => x.AddressLine).Returns("Adaminsæøa");
                userStub.Setup(x => x.CityName).Returns("Ålborg");
                userStub.Setup(x => x.Postcode).Returns("9000");
                userStub.Setup(x => x.Region).Returns(Region.Nordjylland);
                userStub.Setup(x => x.Gender).Returns(Gender.Male);

                User u = null;
                var serviceMock = new Mock<IUserService>();

                serviceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Callback<User>(x => u = x);

                var subject = new UserController(serviceMock.Object);

                subject.Create(userStub.Object);

                Assert.IsTrue(u.FirstName == "Adam" &&
                    u.Gender == Gender.Male &&
                    u.PhoneNumber == "12345678");
            }
            catch
            {
                Assert.Fail();
            }
        }


        #region
        [DataRow("123456789", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (too many characters)
        [DataRow("12345ść", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid phonenumber (not allowed characters)
        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid firstname (not allowed characters)
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamManaż", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid userName (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "he", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //too short userName
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password no capital letter
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid password without number
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male)] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male)] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male)] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male)] //invalid postcode (not allowed characters)
        #endregion
        [TestMethod]
        public void Test_MVCController_Will_Not_Create_A_Movie_With_Invalid_Model_State(string phoneNumber, string firstName,
         string lastName, string email, string userName, string password, string addressLine,
         string cityName, string postCode, Region region, Gender gender)
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var userStub = new Mock<UserModel>();
                //Set up properties
                #region
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
                #endregion
                var subject = new UserController(serviceMock.Object);
                subject.ModelState.AddModelError("RegularExpression", "Doesn't match regex");
                ViewResult resultPage = subject.Create(userStub.Object) as ViewResult;
                Assert.IsTrue("Create" == resultPage.ViewName);
            }
            catch
            {
                Assert.Fail();
            }
        }


        #endregion
        //Read

        [TestMethod]
        public void Search_Of_User_Returns_Redirect_To_Action()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                serviceMock.Setup(t => t.FindUser(It.IsAny<string>())).Returns(new User
                {
                    PhoneNumber = "12345678",
                    FirstName = "Adam",
                    LastName = "Adam",
                    Email = "Adam@gmail.com",
                    UserName = "AdamMana",
                    Password = "Qwerty1",
                    AddressLine = "Streetline",
                    CityName = "Cityname",
                    Postcode = "2154",
                    Region = Region.Nordjylland,
                    Gender = Gender.Male,
                });

                var controller = new HomeController(serviceMock.Object);
                var result = controller.Search(12345678);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Search_Of_User_Entering_Invalid_PhoneNumber_Returns_View_Index()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                serviceMock.Setup(t => t.FindUser(It.IsAny<string>())).Returns(new User
                {
                    PhoneNumber = "12345678",
                    FirstName = "Adam",
                    LastName = "Adam",
                    Email = "Adam@gmail.com",
                    UserName = "AdamMana",
                    Password = "Qwerty1",
                    AddressLine = "Streetline",
                    CityName = "Cityname",
                    Postcode = "2154",
                    Region = Region.Nordjylland,
                    Gender = Gender.Male,
                });

                var controller = new HomeController(serviceMock.Object);
                var result = controller.Search(1234567825) as ViewResult;
                Assert.AreEqual("Index" , result.ViewName);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Search_Of_User__Returns_View_Index_While_Exeption_Occurs()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                serviceMock.Setup(t => t.FindUser(It.IsAny<string>())).Throws(new Exception());
                var controller = new HomeController(serviceMock.Object);
                var result = controller.Search(null) as ViewResult;
                Assert.AreEqual("Index", result.ViewName);
            }
            catch
            {
                Assert.Fail();
            }
        }



        //Update
        /*

        #region

        [TestMethod]//To do
        public void Edit_With_Valid_inputs()
        {
            var userMock = new Mock<UserModel>();
            userMock.Setup(x => x.ID).Returns(1);
            userMock.Setup(x => x.PhoneNumber).Returns("12345678");
            userMock.Setup(x => x.FirstName).Returns("Adam");
            userMock.Setup(x => x.LastName).Returns("Adam");
            userMock.Setup(x => x.Email).Returns("Adam@gmail.com");
            userMock.Setup(x => x.UserName).Returns("AdamMana");
            userMock.Setup(x => x.Password).Returns("Qwerty1");
            userMock.Setup(x => x.AddressLine).Returns("Streetline");
            userMock.Setup(x => x.CityName).Returns("Cityname");
            userMock.Setup(x => x.Postcode).Returns("2154");
            userMock.Setup(x => x.Region).Returns(Region.Midtjylland);
            userMock.Setup(x => x.Gender).Returns(Gender.Male);
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.EditUser(It.IsAny<User>())).Returns(true);
            var controller = new Mock<UserController>(userServiceStub.Object);
            controller.Setup(t => t.Request.Form["Email"]).Returns("Adam@gmail.com");
            controller.Setup(t => t.Request.Form["Password"]).Returns("Qwerty1");
            controller.Setup(t => t.Request.Form["Username"]).Returns("AdamMana");
            controller.Setup(t => t.Request.Form["firstName"]).Returns("Adam");
            controller.Setup(t => t.Request.Form["lastName"]).Returns("Adam");
            controller.Setup(t => t.Request.Form["gender"]).Returns("Male");
            controller.Setup(t => t.Request.Form["phoneNumber"]).Returns("12345678");
            controller.Setup(t => t.Request.Form["addressLine"]).Returns("Streetline");
            controller.Setup(t => t.Request.Form["postcode"]).Returns("2154");
            controller.Setup(t => t.Request.Form["cityName"]).Returns("Cityname");
            controller.Setup(t => t.Request.Form["region"]).Returns("Midtjylland");
            controller.Object.Edit(1);
            userServiceStub.Verify(x => x.EditUser(It.IsAny<User>()), Times.Once());
        }

        //To do
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
        public void Update_UserService_Update_Valid_Inputs(string phoneNumber, string firstName,
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
            subject.ModelState.AddModelError("RegularExpression", "Doesn't match regex");
            ViewResult resultPage = subject.Edit(userStub.Object.ID) as ViewResult;
            Assert.IsTrue("Edit" == resultPage.ViewName);
        }

        #endregion
        */
        //Delete
        #region
        [TestMethod]
        public void Test_Delete_Result_Passing_Invalid_Integer()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);
            var result = controller.Delete(-1);
            Assert.IsNull(result);
        }


        [TestMethod]
        public void Test_Delete_Null_Found()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.DeleteUser(1)).Returns(false);
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Delete(1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_Delete_View_With_Existing_Integer()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.DeleteUser(It.IsAny<int>())).Returns(true);
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Delete(1);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        #endregion


    }
}



