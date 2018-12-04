﻿using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobPortal.Model;
using System.Linq;
using MyWeb.Controllers;
using System.Web.Mvc;
using WebJobPortal.Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace UnitTestProject1.MVC__tests
{
    [TestClass]
    public class OfferTests
    {
        [TestMethod]
        public async void Test_Index_Show_All_Offers()
        {
            Offer[] array = { new Offer(), new Offer(), new Offer() };
            var all = array.AsQueryable<Offer>();

            var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
            serviceMock.Setup(x => x.GetAllOffersAsync()).Returns(Task.FromResult(array));

            var ctr = new ServiceOfferController(serviceMock.Object);
            var result = await ctr.Index(null, 0) as ViewResult;
            ManageOffers[] model = (ManageOffers[])result.Model;
            Assert.AreEqual(3, model.Length);
        }

        [DataRow("Cleaning", 2, DisplayName = "The same cases and word length, 2 maches")] //invalid phonenumber (too many characters)
        [DataRow("Gardening", 2, DisplayName = "Different letter cases.")] //invalid phonenumber (too many characters)
        [DataRow("Garden", 3, DisplayName = "Part of the word(garden/gardening), 2 maches")] //invalid phonenumber (too many characters)
        [DataRow("", 5, DisplayName = "Emptystring")] //invalid phonenumber (too many characters)

        [TestMethod]
        public async void  Test_Index_Show_All_Offers_Which_Contains_searching_string(string searchingString, int foundOffers)
        {
            Offer[] array = { new Offer { Title= "Cleaning at your house"},
                              new Offer {Title = "Very good Cleaning" },
                              new Offer {Title = "GaRDening" },
                              new Offer {Title = "gardening" },
                              new Offer {Title = "garden" }
            };
        

            var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
            serviceMock.Setup(x => x.GetAllOffers()).Returns(array);

            var ctr = new ServiceOfferController(serviceMock.Object);
            var result = await ctr.Index(searchingString, 0) as ViewResult;
            ManageOffers[] model = (ManageOffers[])result.Model;
            Assert.AreEqual(foundOffers, model.Length);
        }

        [TestMethod]
        public void ViewDetails_View_Tests_If_Return_correct_view()
        {
            try
            {
                var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
                serviceMock.Setup(x => x.FindServiceOfferAsync(It.IsAny<int>())).ReturnsAsync(new Offer { Id = 1 });

                var ctr = new ServiceOfferController(serviceMock.Object);
                var task = ctr.ViewDetails(1);
                var result = task.Result as ViewResult;

                Assert.AreEqual("", result.ViewName);
            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void ViewDetails_View_Tests_If_Return_correct_ID()
        {
            try
            {
                var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
                serviceMock.Setup(x => x.FindServiceOfferAsync(It.IsAny<int>())).ReturnsAsync(new Offer { Id = 1 });

                var ctr = new ServiceOfferController(serviceMock.Object);
                var task = ctr.ViewDetails(1);
                var result = task.Result as ViewResult;
                var model = ((ViewDetails)result.Model);
                Assert.AreEqual(1, model.Id);
            }

            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async void ViewDetails_UpdateOffer_View_Tests_If_Rederict_to_Action()
        {
            try
            {
                ViewDetails detail = new ViewDetails();
                detail.Id = 3;
                detail.Title = "234567";
                detail.RatePerHour = 122;
                detail.Description = "2345678765433456";
                
                var controllerContext = new Mock<ControllerContext>();
                var principal = new Moq.Mock<IPrincipal>();
                principal.SetupGet(x => x.Identity.Name).Returns("uname");
                //principal.SetupGet(x => x.Identity.GetUserId()).Returns("uname");
                controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
               
                var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
                serviceMock.Setup(x => x.UpdateServiceOffer(It.IsAny<Offer>())).Returns(true);

                var ctr = new ServiceOfferController(serviceMock.Object);
                ctr.ControllerContext = controllerContext.Object;
                var result = ctr.ViewDetails(detail);//Task<actionRsult>>
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                RedirectToRouteResult routeResult = await result as RedirectToRouteResult;
                Assert.AreEqual(routeResult.RouteValues["action"], "UserProfile");
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Add_View_Tests_If_Return_correct_view()
        {
            try
            {
                var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
                serviceMock.Setup(x => x.CreateServiceOffer(It.IsAny<Offer>())).Returns(true);

                var ctr = new ServiceOfferController(serviceMock.Object);
                var result = (ViewResult)ctr.Add();

                Assert.AreEqual("Add", result.ViewName);
            }

            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Add_View_Tests_If_Rederict_to_Action()
        {
            try
            {
                var controllerContext = new Mock<ControllerContext>();
                var principal = new Moq.Mock<IPrincipal>();
                principal.SetupGet(x => x.Identity.Name).Returns("uname");
                //principal.SetupGet(x => x.Identity.GetUserId()).Returns("uname");
                controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
                WorkingHours[] array = { new WorkingHours() };
                AddServiceOfferModel model = new AddServiceOfferModel();
                ManageOffers manageOffers = new ManageOffers();
                manageOffers.Author = "2";
                manageOffers.Category = Category.Architecture;
                manageOffers.Description = "2222222222222222";
                manageOffers.Id = 22;
                manageOffers.RatePerHour = 111;
                manageOffers.Subcategory = SubCategory.InteriorDesign;
                manageOffers.Title = "my titlee";
                model.ManageOffers = manageOffers;
                model.WorkingDays = array.AsEnumerable<WorkingHours>();
                var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
                serviceMock.Setup(x => x.CreateServiceOfferAsync(It.IsAny<Offer>())).ReturnsAsync(true);

                var ctr = new ServiceOfferController(serviceMock.Object);
                ctr.ControllerContext = controllerContext.Object;
                var task = ctr.Add(model);//Task<actionRsult>>
                var result = task.Result;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.AreEqual(routeResult.RouteValues["action"], "Index");
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Delete_View_Tests_If_Rederict_to_Action()
        {
            try
            {
                var controllerContext = new Mock<ControllerContext>();
                var principal = new Moq.Mock<IPrincipal>();
                principal.SetupGet(x => x.Identity.Name).Returns("uname");
                //principal.SetupGet(x => x.Identity.GetUserId()).Returns("uname");
                controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
                var serviceMock = new Mock<MyWeb.OfferReference.IOfferService>();
                serviceMock.Setup(x => x.DeleteServiceOfferAsync(It.IsAny<int>())).ReturnsAsync(true);

                var ctr = new ServiceOfferController(serviceMock.Object);
                ctr.ControllerContext = controllerContext.Object;
                var task = ctr.Delete(1);//Task<actionRsult>>
                var result = task.Result;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.AreEqual(routeResult.RouteValues["action"], "UserProfile");
            }
            catch
            {
                Assert.Fail();
            }
        }
        }
}
