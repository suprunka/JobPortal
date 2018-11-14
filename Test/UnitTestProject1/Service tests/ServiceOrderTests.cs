using System;
using System.Threading;
using System.Threading.Tasks;
using JobPortal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLibrary.Services;

namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class ServiceOrderTests
    {


        [TestMethod]
        public void Test_Concurrency_While_Creating_Order()
        {
            /*var user1 = new Mock<User>();
            user1.SetupAllProperties();
            var user2 = new Mock<User>();
            user2.SetupAllProperties();
            var offer1 = new Mock<Offer>();
            offer1.SetupAllProperties();
            var offer2 = new Mock<Offer>();
            offer2.SetupAllProperties();
            OrderService service = new OrderService();
            Thread[] ts = new Thread[2];

            Task task = Task.Factory.StartNew(() =>
            service.CreateOrder(user1.Object, offer1.Object, 20));
            Task task2 = Task.Factory.StartNew(() =>
            service.CreateOrder(user2.Object, offer1.Object, 50));
            task.Wait(1000);*/



        }
    }
}

