using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using JobPortal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyWeb.Controllers;
using MyWeb.OfferReference;
using MyWeb.OrderReference;
using MyWeb.UserReference1;

namespace UnitTestProject1.MVC__tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void CreateOrder_With_null_customer_Id()
        {
            try
            {
                var serviceMockOrder = new Mock<IOrderService>();
                var serviceMockUser = new Mock<IUserService>();

                var ctr = new OrderController(serviceMockUser.Object, serviceMockOrder.Object);
                var task = ctr.CreateOrder(null);
                var result = task.Result as ViewResult;

                Assert.AreEqual(result, null);
            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void CreateOrder_With_Error_While_Creating()
        {
            Task<ActionResult> task = null;
            try
            {
                var serviceMockOrder = new Mock<IOrderService>();
                var serviceMockUser = new Mock<IUserService>();
                serviceMockOrder.Setup(x => x.CreateOrderAsync(It.IsAny<string>())).Throws(new FaultException());

                var ctr = new OrderController(serviceMockUser.Object, serviceMockOrder.Object);
                task = ctr.CreateOrder("22");
                Assert.Fail();

            }

            catch
            {
                var result = task.Result;
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

                Assert.AreEqual(routeResult.RouteValues["action"], "Index");
            }
        }
        [TestMethod]
        public void AddToCart_CorrectAdding()
        {
            try
            {
                var serviceMockOrder = new Mock<IOrderService>();
                var serviceMockUser = new Mock<IUserService>();
                serviceMockOrder.Setup(x => x.AddToCartAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).ReturnsAsync(true);

                var ctr = new OrderController(serviceMockUser.Object, serviceMockOrder.Object);
                var task = ctr.AddToCart("2", 2, DateTime.Now, new TimeSpan(11, 00, 00), new TimeSpan(12, 00, 00));

                var result = task.Result;
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

                Assert.AreEqual(routeResult.RouteValues["action"], "Index");
            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void AddToCart_error_view()
        {
            try
            {
                var serviceMockOrder = new Mock<IOrderService>();
                var serviceMockUser = new Mock<IUserService>();
                serviceMockOrder.Setup(x => x.AddToCartAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).ReturnsAsync(false);

                var ctr = new OrderController(serviceMockUser.Object, serviceMockOrder.Object);
                var task = ctr.AddToCart("2", 2, DateTime.Now, new TimeSpan(11, 00, 00), new TimeSpan(12, 00, 00));

                var result = task.Result as ViewResult;

                Assert.AreEqual("Error", result.ViewName);

            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Clean_cartCorrect()
        {
            try
            {
                var serviceMockOrder = new Mock<IOrderService>();
                var serviceMockUser = new Mock<IUserService>();
                serviceMockOrder.Setup(x => x.CleanCartAsync(It.IsAny<string>())).ReturnsAsync(true);

                var ctr = new OrderController(serviceMockUser.Object, serviceMockOrder.Object);
                var task = ctr.CleanCart("2");

                var result = task.Result;
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

                Assert.AreEqual(routeResult.RouteValues["action"], "Index");
            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Cleancart_error_view()
        {
            try
            {
                var serviceMockOrder = new Mock<IOrderService>();
                var serviceMockUser = new Mock<IUserService>();
                serviceMockOrder.Setup(x => x.CleanCartAsync(It.IsAny<string>())).ReturnsAsync(false);

                var ctr = new OrderController(serviceMockUser.Object, serviceMockOrder.Object);
                var task = ctr.CleanCart("2");

                var result = task.Result;

                Assert.AreEqual(null, result);

            }

            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void DeleteFromCart()
        {
            //DeleteFromCard(string idU, int? id, DateTime? date, TimeSpan? from, TimeSpan? to)

            try
            {
                var serviceMockOrder = new Mock<IOrderService>();
                var serviceMockUser = new Mock<IUserService>();
                serviceMockOrder.Setup(x => x.DeleteFromCartAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).ReturnsAsync(true);

                var ctr = new OrderController(serviceMockUser.Object, serviceMockOrder.Object);
                var task = ctr.DeleteFromCard("2", 2, DateTime.Now, new TimeSpan(11, 00, 00), new TimeSpan(12, 00, 00));
                var result = task.Result;
                RedirectToRouteResult routeResult = result as RedirectToRouteResult;
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

                Assert.AreEqual(routeResult.RouteValues["action"], "Index");

            }

            catch
            {
                Assert.Fail();
            }
        }


    }
}
