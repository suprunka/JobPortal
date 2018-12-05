using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using JobPortal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Repository.DbConnection;
using ServiceLibrary;

namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class ServiceOrderTests
    {
        //GetShoppingCart
        [TestMethod]
        public void Get_Shopping_Cart_Is_Not_Null_If_Found()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetShoppingCart(It.IsAny<string>())).Returns(new List<ShoppingCart>()
                {
                    new ShoppingCart
                {
                    Date = new DateTime(2018,12,12),
                    HourFrom = new TimeSpan(13,0,0),
                    HourTo = new TimeSpan(18,0,0),
                }});

                databaseMock.Setup(y => y.Offers.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
                {
                    Employee_ID = "123",
                    ID = 21,
                    Description = "Description",
                    RatePerHour = 32,
                    Title = "Title",
                });

                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetShoppingCart("123");
                Assert.IsNotNull(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Get_Empty_Shopping_Cart_If_Found_Is_Null()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetShoppingCart(It.IsAny<string>())).Returns(new List<ShoppingCart>());
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetShoppingCart("123");
                Assert.AreEqual(0, result.List.Count);
            }
            catch
            {
                Assert.Fail();
            }
        }
        
        //CreateOrder
        [TestMethod]
        public void Create_Order_Returns_Order_Object()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.CreateOrder(It.IsAny<string>())).Returns(new OrderTable()
                {
                    ID = 3,
                    TotalPrice = 20,
                    OrderStatus = new OrderStatus
                    {
                        Order_status = "In process",
                    },
                    Users_ID = "sa",
                });
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.CreateOrder("123");
                Assert.AreEqual("sa", result.User_ID);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Create_Order_Returns_FaultException_When_BookedTimeException_Cought()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.CreateOrder(It.IsAny<string>())).Throws(new BookedTimeException(21, "das", new TimeSpan(12, 0, 0), new TimeSpan(19, 0, 0), new DateTime(2018,12,12), "exception"));
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.CreateOrder("123");
                Assert.Fail();
            }
            catch(FaultException<BookedTimeException>)
            {
                Assert.IsTrue(true);
            }
        }

        //AddToCart
        [TestMethod]
        public void AddToCart_Returns_True_If_Database_Adds()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.AddToCart(It.IsAny<ShoppingCart>())).Returns(true);
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.AddToCart("dsa", 21, new DateTime(2018,12,12), new TimeSpan(13,0,0), new TimeSpan(18,0,0));
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void AddToCart_Returns_False_If_Database_Fails_To_Add()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.AddToCart(It.IsAny<ShoppingCart>())).Returns(false);
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.AddToCart("dsa", 21, new DateTime(2018, 12, 12), new TimeSpan(13, 0, 0), new TimeSpan(18, 0, 0));
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
        }

        //DeleteFromCart
        [TestMethod]
        public void DeleteFromcart_Returns_True_If_Database_Deletes()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.DeleteFromCart(It.IsAny<ShoppingCart>())).Returns(true);
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.DeleteFromCart("dsa", 21, new DateTime(2018, 12, 12), new TimeSpan(13, 0, 0), new TimeSpan(18, 0, 0));
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void DeleteFromCart_Returns_False_If_Database_Fails_To_Add()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.DeleteFromCart(It.IsAny<ShoppingCart>())).Returns(false);
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.DeleteFromCart("dsa", 21, new DateTime(2018, 12, 12), new TimeSpan(13, 0, 0), new TimeSpan(18, 0, 0));
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
        }

        //CancelOrder
        [TestMethod]
        public void CancelOrder_Returns_True_If_Database_Cancels()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.CancelOrder(It.IsAny<Order>())).Returns(true);
                OrderService service = new OrderService(databaseMock.Object);
                Order o = new Order();
                var result = service.CancelOrder(o);
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void CancelOrder_Returns_False_If_Database_Fails_To_Cancel()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.CancelOrder(It.IsAny<Order>())).Returns(false);
                OrderService service = new OrderService(databaseMock.Object);
                Order o = new Order();
                var result = service.CancelOrder(o);
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
        }

        //PayForOrder
        [TestMethod]
        public void PayForOrder_Returns_True_If_Database_Returns_True()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.PayForOrder(It.IsAny<OrderTable>())).Returns(new OrderTable
                {
                    ID = 32,
                });
                OrderService service = new OrderService(databaseMock.Object);
                Order o = new Order
                {
                    ID = 32,
                };
                var result = service.PayForOrder(o);
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void PayForOrder_Returns_False_If_Database_Returns_False()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.PayForOrder(It.IsAny<OrderTable>())).Returns((OrderTable)null);
                OrderService service = new OrderService(databaseMock.Object);
                Order o = new Order{
                    ID = 52,
                };
                var result = service.PayForOrder(o);
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
        }

        //GetHoursFrom
        [TestMethod]
        public void GetHoursFrom_Returns_Hours()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetHoursFrom(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(new List<TimeSpan> { 
                    new TimeSpan(13, 0, 0), new TimeSpan(18,0,0)});
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetHoursFrom(32, new DateTime(2018,12,12)).GetEnumerator();
                int number = 0;
                while (result.MoveNext())
                {
                    number++;
                }
                Assert.AreEqual(2, number);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetHoursFrom_Returns_EmptyList()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetHoursFrom(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(new List<TimeSpan>());
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetHoursFrom(32, new DateTime(2018, 12, 12)).GetEnumerator();
                int number = 0;
                while (result.MoveNext())
                {
                    number++;
                }
                Assert.AreEqual(0, number);
            }
            catch
            {
                Assert.Fail();
            }
        }

        //GetHoursTo
        [TestMethod]
        public void GetHoursTo_Returns_Hours()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetHoursTo(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>())).Returns(new List<TimeSpan> {
                    new TimeSpan(13, 0, 0), new TimeSpan(18,0,0)});
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetHoursTo(32, new DateTime(2018, 12, 12), new TimeSpan(13,0,0)).GetEnumerator();
                int number = 0;
                while (result.MoveNext())
                {
                    number++;
                }
                Assert.AreEqual(2, number);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetHoursTo_Returns_EmptyList()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetHoursTo(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>())).Returns(new List<TimeSpan>());
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetHoursTo(32, new DateTime(2018, 12, 12), new TimeSpan(13, 0, 0)).GetEnumerator();
                int number = 0;
                while (result.MoveNext())
                {
                    number++;
                }
                Assert.AreEqual(0, number);
            }
            catch
            {
                Assert.Fail();
            }
        }
        
        //CleanCart
        [TestMethod]
        public void CleanCart_Returns_True_If_Cart_Was_Empited()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.CleanCart(It.IsAny<string>())).Returns(true);
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.CleanCart("123");
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void CleanCart_Returns_False_If_Cart_To_Be_Cleaned_Was_Not_Found()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.CleanCart(It.IsAny<string>())).Returns(false);
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.CleanCart("123");
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
        }

        //GetJobCallendar
        [TestMethod]
        public void GetJobCallednar_Returns_List_Of_JobOffers_If_Database_Returns_Data()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetJobCalendar(It.IsAny<DateTime>(), It.IsAny<string>())).Returns(new List<Salelines> {
                    new Salelines
                    {
                        OrderTable = new OrderTable
                        {
                            Users_ID= "123",
                        },
                        BookedDate = new BookedDate
                        {
                            HourFrom = new TimeSpan(13,0,0),
                            HourTo = new TimeSpan(17,0,0),
                        },
                        ServiceOffer_ID = 1,
                        ServiceOffer = new ServiceOffer
                        {
                            Employee_ID = "123",
                            SubCategory = new Repository.DbConnection.SubCategory
                            {
                                Name= "Cleaning",
                                Category = new Repository.DbConnection.Category
                                {
                                    Name ="Home",
                                }
                            },
                            Description = "Description",
                            RatePerHour = 23,
                            Title = "Title",
                        }
                    }}.AsQueryable());
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetJobCallendar(new DateTime(2018, 12, 12), "123").GetEnumerator();
                int number = 0;
                while (result.MoveNext())
                {
                    number++;
                }
                Assert.AreEqual(1, number);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetJobCallendar_Returns_Empty_List_If_Database_Returns_Nothing()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.GetJobCalendar(It.IsAny<DateTime>(), It.IsAny<string>())).Returns(new List<Salelines>().AsQueryable());
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.GetJobCallendar(new DateTime(2018, 12, 12), "123").GetEnumerator();
                int number = 0;
                while (result.MoveNext())
                {
                    number++;
                }
                Assert.AreEqual(0, number);
            }
            catch
            {
                Assert.Fail();
            }
        }
        
        //FindOrder
        [TestMethod]
        public void FindOrder_Returns_Order_Object_If_One_Was_Found()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.Get(It.IsAny<Expression<Func<OrderTable, bool>>>())).Returns(new OrderTable
                {
                    ID = 21,
                });
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.FindOrder("123");
                Assert.AreEqual(21, result.ID);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void FindOrder_Returns_Null_If_Order_Object_Was_Not_Found()
        {
            try
            {
                var databaseMock = new Mock<IUnitOfWork>();
                databaseMock.Setup(x => x.Orders.Get(It.IsAny<Expression<Func<OrderTable, bool>>>())).Returns((OrderTable)null);
                OrderService service = new OrderService(databaseMock.Object);
                var result = service.FindOrder("123");
                Assert.IsNull(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}