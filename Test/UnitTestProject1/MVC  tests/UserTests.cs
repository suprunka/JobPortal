using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


using WebJobPortal.Controllers;
using System.Web.Mvc;
using WebJobPortal.UserServiceReference;

namespace UnitTestProject1.MVC__tests
{
    [TestClass]
    public class UserTests
    {
        //Create
        #region
        #endregion
        [TestMethod]
        public void Test_Create_View()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(s => s.CreateUser(null));
            var controller = new UserController(serviceMock.Object);
            var result = controller.Create();
            Assert.IsNotNull(result);

        }
    }

      /*  [TestMethod]
        public void Test_Create_View_Passing_A_Valid_Object()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);
            var result = controller.Create(new UserWebModel
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
            }) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Create_View_Exception_Expected()
        {

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
            var userServiceStub = new UserWebModel
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

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(userServiceStub, null, null);
            var result = new List<ValidationResult>();


            var isModelStateValid = Validator.TryValidateObject(userServiceStub, context, result, true);

            Assert.IsTrue(isModelStateValid == shouldValidate);
        }


        [TestMethod]
        public void Test_MVCController_Can_Create_User_With_Valid_Inputs()
        {
            var userStub = new Mock<UserWebModel>().SetupAllProperties();
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

            Users u = null;
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(x => x.CreateUser(It.IsAny<Users>())).Callback<Users>(x => u = x);

            var subject = new UserController(serviceMock.Object);

            subject.Create(userStub.Object);

            Assert.IsTrue(u.FirstName == "Adam" &&
                u.Gender == Gender.Male &&
                u.PhoneNumber == "12345678");
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
            var serviceMock = new Mock<IUserService>();
            var userStub = new Mock<UserWebModel>();
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


        [TestMethod]
        public void Test_Service_Creation_Of_User_Hit_Database_Once()
        {
            var userStub = new Mock<Users>();
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

            var databaseMock = new Mock<IRepository<Users>>();
            UserService service = new UserService(databaseMock.Object);
            service.CreateUser(userStub.Object);
            databaseMock.Verify(t => t.Create(It.IsAny<Users>()), Times.AtLeastOnce);
        }


        [TestMethod]
        public void Test_Service_Creation_Of_User_Is_Not_Null()
        {
            var userStub = new Mock<Users>();
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
            var databaseMock = new Mock<IRepository<Users>>();
            UserService service = new UserService(databaseMock.Object);
            Users u = service.CreateUser(userStub.Object);
            Assert.IsNotNull(u);
        }
        #endregion

        //Read
        #region
        /*[TestMethod]
        [DataRow("1", 1, DisplayName = "Valid ID ")]
        [DataRow("", 3, DisplayName = "Empty id ")]
       /* public void Index_Will_return_the_correct_no_of_users_on_search(string Id, int expectedNoOfResults)
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetAll()).Returns(() =>
            {
             //   return new IEnumerable<Users> { new Users() { ID = 1 }, new Users() { ID = 71 }, new Users() { ID = 10 } };
            });

            var sut = new UserController(userServiceStub.Object);
            ViewResult resultPage = sut.Index(Id) as ViewResult;

            var model = resultPage.ViewData.Model as IEnumerable<Users>;
            Assert.IsTrue(model.Count() == expectedNoOfResults);
        }

        [TestMethod]
        public void Index_Will_show_all_movies_from_service()
        {
            var userServiceStub = new Mock<IUserService>();
          
            userServiceStub.Setup(x => x.GetAll()).Returns(() =>
            {
            //    return new IEnumerable<Users> { new Users(), new Users(), new Users() };
            });
            var sut = new UserController(userServiceStub.Object);

            var resPage = sut.Index(null) as ViewResult;

            var model = resPage.ViewData.Model as IEnumerable<Users>;

            Assert.IsTrue(model.Count() == 3);
        }
        #endregion

        //Update
        #region



    [TestMethod]
        public void Edit_With_Valid_inputs()
        {
            var userMock = new Mock<UserWebModel>();
            userMock.Setup(x => x.LastName).Returns("Dreuk");
            userMock.Setup(x => x.PhoneNumber).Returns("12334455");
            var userServiceStub = new Mock<IUserService>();
            Users userSenftoService = null;

            userServiceStub.Setup(x => x.EditUser(It.IsAny<Users>()))
                .Callback<Users>(x => userSenftoService = x);

            var sut = new UserController(userServiceStub.Object);
            sut.Edit(userMock.Object);
            userServiceStub.Verify(x =>
           x.EditUser(It.IsAny<Users>()), Times.Once());

            Assert.IsTrue(
                userSenftoService.LastName == "Dreuk" &&
                userSenftoService.PhoneNumber == "12334455"
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
        public void Update_UserService_Update_Valid_Inputs(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender, bool shouldValidate)
        {
            var serviceMock = new Mock<IUserService>();
            var userStub = new Mock<UserWebModel>().SetupAllProperties();
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
            ViewResult resultPage = subject.Edit(userStub.Object) as ViewResult;
            Assert.IsTrue("Edit" == resultPage.ViewName);
        }

        #endregion

        //Delete
        #region
        [TestMethod]
        public void Test_Delete_Result_Passing_Negative_Integer()
        {
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);
            var result = controller.Delete(-1) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Test_Delete_Null_Found()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.FindUser(1)).Returns(() =>
            {
                return null;
            });
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Delete(1) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Test_Delete_View_With_Existing_Integer()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.FindUser(1)).Returns(() =>
            {
                return new Users { ID = 1 };
            });
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Delete(1) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Test_Delete_Invokes_Exception()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.FindUser(1)).Returns(() =>
            {
                throw new Exception();
            });
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Delete(1) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void Test_DeleteConfirm_While_User_Is_Null_Should_Return_Bad_Request()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.FindUser(1)).Returns(() =>
            {
                return null;
            });
            var controller = new UserController(userServiceStub.Object);
            var result = controller.DeleteConfirm(null) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void Test_DeleteConfirm_While_User_Is_Valid_Returns_Redirection()
        {
            var userServiceStub = new Mock<IUserService>();
            Users userMock = null;
            userServiceStub.Setup(x => x.FindUser(1)).Returns(() =>
            {
                userMock = new Users { ID = 1 };
                return userMock;
            });
            var controller = new UserController(userServiceStub.Object);
            var result = controller.DeleteConfirm(AutoMapper.Mapper.Map(userMock, new UserWebModel())) as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }


        [TestMethod]
        public void Test_Service_Delete_Of_User_Hit_Database_Once()
        {
            var userStub = new Mock<Users>();
            userStub.Setup(x => x.FirstName).Returns("Adam");
            userStub.Setup(x => x.PhoneNumber).Returns("12345678");
            userStub.Setup(x => x.LastName).Returns("Adam");
            userStub.Setup(x => x.Email).Returns("adam@gmail.com");
            userStub.Setup(x => x.UserName).Returns("Adammana");
            userStub.Setup(x => x.Password).Returns("Adama1");
            userStub.Setup(x => x.AddressLine).Returns("Adaminsæøa");
            userStub.Setup(x => x.CityName).Returns("Ålborg");
            userStub.Setup(x => x.Postcode).Returns("9000");
            userStub.Setup(x => x.ID).Returns(256548);
            var databaseMock = new Mock<IRepository<Users>>();
            UserService service = new UserService(databaseMock.Object);
            bool result = service.DeleteUser(userStub.Object.ID);
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Test_Service_Delete_Of_User()
        {
            var userMock = new Mock<Users>();
            userMock.SetupAllProperties();
            var databaseMock = new Mock<IRepository<Users>>();
            UserService service = new UserService(databaseMock.Object);
            bool result = service.DeleteUser(2);
            Assert.IsTrue(result);
        }
        #endregion




    }
*/
}
