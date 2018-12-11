using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNet.Identity;
using Moq;
using System.Web.Mvc;
using JobPortal.Model;
using WebJobPortal.Models;
using System;
using MyWeb.Controllers;
using MyWeb.UserReference1;
using MyWeb.OfferReference;
using System.Security.Principal;
using MyWeb.OrderReference;
using PagedList;
using WebJobPortal.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.Owin.Security;

namespace UnitTestProject1.MVC__tests
{
    [TestClass]
    public class UserTests
    {



        [TestMethod]
        public void Test_UserProfile_View()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.FindUser(It.IsAny<String>())).Returns(new User { ID = 2 });
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                var result = controller.UserProfile("2").Result as ViewResult;
                Assert.IsTrue("" == result.ViewName);
            }
            catch
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void Test_Edit_Get_Proper_User()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.FindUserByIDAsync(It.IsAny<int>())).ReturnsAsync(new User { ID = 2 });
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                var task = controller.Edit(2);//Task<actionRsult>>
                var result = (ViewResult)task.Result;
                var model = result.Model as UserProfileViewModel;
                Assert.AreEqual("2", model.ID);
            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test_Edit_View()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.FindUserByIDAsync(It.IsAny<int>())).ReturnsAsync(new User { ID = 2 });
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                var task = controller.Edit(2);//Task<actionRsult>>
                var result = (ViewResult)task.Result;

                Assert.AreEqual("", result.ViewName);
            }

            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Test_Edit_View_OfProper_User()
        {
            try
            {
                var controllerContext = new Mock<ControllerContext>();
                var principal = new Moq.Mock<IPrincipal>();
                principal.SetupGet(x => x.Identity.Name).Returns("uname");
                //principal.SetupGet(x => x.Identity.GetUserId()).Returns("uname");
                controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

                ManageOfferModel[] array = { new ManageOfferModel(), new ManageOfferModel() };
                UserProfileViewModel user = new UserProfileViewModel();
                user.Email = "ww@wp.pl";
                user.FirstName = "firstname";
                user.Gender = Gender.Female;
                user.ID = "1";
                user.LastName = "lastname";
                user.PayPalMail = "paypal.pl";
                user.PhoneNumber = "90807090";
                user.Postcode = "9000";
                user.Region = Region.Hovedstaden;
                user.Services = array.ToPagedList(5, 1);
                user.UserName = "uname";
                user.AddressLine = "ww";
                user.CityName = "Aalborg";
                user.Description = "this is description";
                //mock.Setup(x => x.Services).Returns(new MyWeb.Models.ManageOffers[3]);
                //UserProfileViewModel u = new UserProfileViewModel { Services = new MyWeb.Models.ManageOffers[4],  }

                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.EditUserAsync(It.IsAny<User>())).ReturnsAsync(true);
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                controller.ControllerContext = controllerContext.Object;

                var task = controller.Edit(user);//Task<actionRsult>>
                var result = task.Result;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                //  Assert.AreEqual(routeResult.RouteValues["UserProfile"], "asd");


            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test_AddDescription_View()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.FindUserByIDAsync(It.IsAny<int>())).ReturnsAsync(new User { ID = 2 });
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                var task = controller.AddDescription(2);//Task<actionRsult>>
                var result = (ViewResult)task.Result;

                Assert.AreEqual("", result.ViewName);
            }

            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Test_AddDescription_checking()
        {
            try
            {
                var controllerContext = new Mock<ControllerContext>();
                var principal = new Moq.Mock<IPrincipal>();
                principal.SetupGet(x => x.Identity.Name).Returns("uname");
                //principal.SetupGet(x => x.Identity.GetUserId()).Returns("uname");
                controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
                var dmock = new Mock<ChangeDescriptionViewModel>();
                dmock.SetupAllProperties();
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.AddDescriptionAsync(It.IsAny<User>())).ReturnsAsync(true);
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                controller.ControllerContext = controllerContext.Object;

                var task = controller.AddDescription(dmock.Object);//Task<actionRsult>>
                var result = task.Result;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.AreEqual(routeResult.RouteValues["action"], "UserProfile");
                Assert.AreEqual(routeResult.RouteValues["controller"], "User");


            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Change_Mail_view()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.FindUserByIDAsync(It.IsAny<int>())).ReturnsAsync(new User
                {
                });
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                var task = controller.ChangeEmail(2);//Task<actionRsult>>
                var result = (ViewResult)task.Result;

                Assert.AreEqual("", result.ViewName);
            }

            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ChangeMail_check_if_redirect_to_correct_action()
        {
            try
            {
                var controllerContext = new Mock<ControllerContext>();
                var principal = new Moq.Mock<IPrincipal>();
                principal.SetupGet(x => x.Identity.Name).Returns("uname");
                controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

                ChangeEmailViewModel mail = new ChangeEmailViewModel();
                mail.NewEmail = "new@mail.pl";
                mail.OldEmail = "www@w.pl";
                mail.UserProfileViewModel = new UserProfileViewModel();
                mail.Id = 2;
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
                serviceMock.Setup(s => s.EditUserEmailAsync(It.IsAny<User>())).ReturnsAsync(true);
                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                controller.ControllerContext = controllerContext.Object;

                var task = controller.ChangeEmail(mail);//Task<actionRsult>>
                var result = task.Result;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.AreEqual(routeResult.RouteValues["action"], "UserProfile");
                Assert.AreEqual(routeResult.RouteValues["controller"], "User");


            }

            catch
            {
                Assert.Fail();
            }
        }



        [TestMethod]
        public void Test_Create_View__With_No_Cookie_Exception_Expected()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(x => x.CreateUser(It.IsAny<User>(), It.IsAny<String>())).Throws(new Exception());
            var controller = new AccountController(serviceMock.Object);
            Assert.ThrowsException<NullReferenceException>(() => controller.SetUserProperties(new SetPropertiesViewModel
            {
                FirstName = "Adam",
                LastName = "Adam",
                AddressLine = "Streetline",
                CityName = "Cityname",
                Postcode = "2154",
                PayPalMail = "adamek@wp.pl",
                Region = Region.Nordjylland,
                Gender = Gender.Male,
            }) as ViewResult);
        }



        [DataRow("Adaś", "Adam", "Streetline", "Cityname", "2154", "paypal@wp.pl", Region.Hovedstaden, Gender.Male, false)] //invalid firstname (not allowed characters)
        [DataRow("Adam", "Adamżść", "Streetline", "Cityname", "2154", "paypal@wp.pl", Region.Hovedstaden, Gender.Male, false)] //invalid lastname (not allowed characters)
        [DataRow("Adam", "Adam", "Streetlineżć", "Cityname", "2154", "paypal@wp.pl", Region.Hovedstaden, Gender.Male, false)] //invalid addressline (not allowed characters)
        [DataRow("Adam", "Adam", "Streetline", "Citynamę", "2154", "paypal@wp.pl", Region.Hovedstaden, Gender.Male, false)] //invalid city name (not allwed characters)
        [DataRow("Adam", "Adam", "Streetline", "Cityname", "215214", "paypal@wp.pl", Region.Hovedstaden, Gender.Male, false)] //invalid postcode (too long)
        [DataRow("Adam", "Adam", "Streetline", "Cityname", "śćęż", "paypal@wp.pl", Region.Hovedstaden, Gender.Male, false)] //invalid postcode (not allowed characters)
        [DataRow("Adam", "Adam", "Streetline", "Cityname", "2164", "pąypął@wp.pl", Region.Hovedstaden, Gender.Male, false)] //invalid postcode (not allowed characters)
        [DataRow("Adam", "Adam", "Streetline", "Cityname", "2154", "paypal@wp.pl", Region.Hovedstaden, Gender.Male, true)] //valid all data
        [TestMethod]
        public void Test_UserWebModel_validation(string firstName,
          string lastName, string addressLine, string cityName, string postCode, string paypal, Region region, Gender gender, bool shouldValidate)
        {
            try
            {
                var userServiceStub = new SetPropertiesViewModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    AddressLine = addressLine,
                    CityName = cityName,
                    Postcode = postCode,
                    Region = region,
                    Gender = gender,
                    PayPalMail = paypal,
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
                var controllerContext = new Mock<ControllerContext>();
                var principal = new Moq.Mock<IPrincipal>();
                principal.SetupGet(x => x.Identity.Name).Returns("uname");
                controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

                var userStub = new Mock<SetPropertiesViewModel>().SetupAllProperties();
                userStub.Setup(x => x.FirstName).Returns("Adam");
                userStub.Setup(x => x.LastName).Returns("Adam");
                userStub.Setup(x => x.AddressLine).Returns("Adaminsæøa");
                userStub.Setup(x => x.CityName).Returns("Ålborg");
                userStub.Setup(x => x.Postcode).Returns("9000");
                userStub.Setup(x => x.Region).Returns(Region.Nordjylland);
                userStub.Setup(x => x.Gender).Returns(Gender.Male);

                var serviceMock = new Mock<IUserService>();

                serviceMock.Setup(x => x.CreateUser(It.IsAny<User>(), It.IsAny<String>())).Returns(true);

                var subject = new AccountController(serviceMock.Object);
                subject.ControllerContext = controllerContext.Object;
                var result = subject.SetUserProperties(userStub.Object);

                Assert.IsInstanceOfType(result, typeof(ActionResult));
            }
            catch
            {
                Assert.Fail();
            }
        }

        //Read
        [TestMethod]
        public void Search_Of_User_Returns_Redirect_To_View_Result()
        {
            try
            {
                var serviceMock = new Mock<IUserService>();
                var serviceOfferMock = new Mock<IOfferService>();
                var serviceOrderMock = new Mock<IOrderService>();
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

                var controller = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
                var result = controller.UserProfile("dsadv323").Result as ActionResult;
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
            catch
            {
                Assert.Fail();
            }
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
        [TestMethod]
        public void Update_UserService_Update_Valid_Inputs(string phoneNumber, string firstName,
        string lastName, string email, string userName, string password, string addressLine,
        string cityName, string postCode, Region region, Gender gender, bool shouldValidate)
        {

            var serviceMock = new Mock<IUserService>();
            var serviceOfferMock = new Mock<IOfferService>();
            var serviceOrderMock = new Mock<IOrderService>();
            serviceMock.Setup(x => x.EditUserAsync(It.IsAny<User>())).ReturnsAsync(true);
            var userStub = new Mock<UserProfileViewModel>().SetupAllProperties();

            userStub.Setup(x => x.FirstName).Returns(firstName);
            userStub.Setup(x => x.LastName).Returns(lastName);
            userStub.Setup(x => x.AddressLine).Returns(addressLine);
            userStub.Setup(x => x.CityName).Returns(cityName);
            userStub.Setup(x => x.Postcode).Returns(postCode);
            userStub.Setup(x => x.Region).Returns(region);
            userStub.Setup(x => x.Gender).Returns(gender);

            var subject = new UserController(serviceMock.Object, serviceOfferMock.Object, serviceOrderMock.Object);
            subject.ModelState.AddModelError("RegularExpression", "Doesn't match regex");
            ViewResult resultPage = subject.Edit(userStub.Object).Result as ViewResult;
            Assert.IsTrue(userStub.Object == resultPage.Model);
        }


        //Delete
        #region
        [TestMethod]
        public void Test_Delete_Result_Passing_Invalid_Integer_Returns_Null()
        {
            var userServiceStub = new Mock<IUserService>();
            var serviceOfferMock = new Mock<IOfferService>();
            var serviceOrderMock = new Mock<IOrderService>();
            userServiceStub.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(false);
            var controller = new UserController(userServiceStub.Object, serviceOfferMock.Object, serviceOrderMock.Object);
            var result = controller.DeleteAsync(-1);
            Assert.IsNull(result.Result);
        }


        [TestMethod]
        public void Test_Delete_Null_Found()
        {
            var userServiceStub = new Mock<IUserService>();
            var serviceOfferMock = new Mock<IOfferService>();
            var serviceOrderMock = new Mock<IOrderService>();
            userServiceStub.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(false);
            var controller = new UserController(userServiceStub.Object, serviceOfferMock.Object, serviceOrderMock.Object);
            var result = controller.DeleteAsync(1);
            Assert.IsNull(result.Result);
        }

        [TestMethod]
        public void Test_Delete_View_With_Existing_Integer()
        {
            var userServiceStub = new Mock<IUserService>();
            var serviceOfferMock = new Mock<IOfferService>();
            var serviceOrderMock = new Mock<IOrderService>();
            userServiceStub.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(true);
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(x => x.SignOut()).Callback(() => mockAuthenticationManager.Object.SignOut(DefaultAuthenticationTypes.ApplicationCookie)).Verifiable();
            var controller = new UserController(userServiceStub.Object, serviceOfferMock.Object, serviceOrderMock.Object, mockAuthenticationManager.Object);
            var result = controller.DeleteAsync(1);
            Assert.IsInstanceOfType(result.Result, typeof(RedirectToRouteResult));
        }

        #endregion


    }
}




