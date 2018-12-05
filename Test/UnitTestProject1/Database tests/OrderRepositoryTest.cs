using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressTable = Repository.DbConnection.AddressTable;
using Category = Repository.DbConnection.Category;
using Gender = Repository.DbConnection.Gender;
using AspNetUser = Repository.DbConnection.AspNetUsers;
using ServiceOffer = Repository.DbConnection.ServiceOffer;
using ShoppingCart = Repository.DbConnection.ShoppingCart;
using SubCategory = Repository.DbConnection.SubCategory;
using WorkingDates = Repository.DbConnection.WorkingDates;
using Users = Repository.DbConnection.Users;
using UnitTestProject1.Database_tests;
using Repository;
using JobPortal.Model;

namespace UnitTestProject1
{
    //Works
    [TestClass]
    public class OrderRepositoryTest
    {
        private static Users GetUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                AspNetUsers = new AspNetUser
                {
                    PasswordHash = "Adama1",
                    UserName = "Username12",
                    PhoneNumber = "12345670",
                    Email = "adam2@gmail.com",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 4,
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },
                PayPalMail = "mama@wp.pl",
                FirstName = "Adam",
                LastName = "Adam",
                AddressLine = "mickiewicza",

            };
            return userStub;

        }

        private static ServiceOffer GetServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                SubCategory = new SubCategory
                {
                    Name = "Cleaning",
                    Category = new Category
                    {
                        Name = "Home",
                    },
                },
                RatePerHour = 20,
                Description = "Sample",
                Employee_ID = "Username12",
                Title = "First",
            };
            return ServiceOffertub;
        }

        private static ShoppingCart GetShoppingCart()
        {
            var shoppingcart = new ShoppingCart
            {
                Date = new DateTime(2018, 12, 10),
                HourFrom = new TimeSpan(14, 0, 0),
                HourTo = new TimeSpan(16, 0, 0),
            };
            return shoppingcart;
        }

        private static ShoppingCart GetShoppingCartLate()
        {
            var shoppingcart = new ShoppingCart
            {
                Date = new DateTime(2018, 12, 10),
                HourFrom = new TimeSpan(18, 0, 0),
                HourTo = new TimeSpan(20, 0, 0),
            };
            return shoppingcart;
        }

        private static WorkingDates GetWorkingDates()
        {
            var workingdates = new WorkingDates
            {
                HourFrom = new TimeSpan(12, 0, 0),
                HourTo = new TimeSpan(20, 0, 0),
                NameOfDay = DayOfWeek.Monday.ToString(),
            };
            return workingdates;
        }
    
        private static WorkingDates GetWorkingDatesSecond()
        {
            var workingdates = new WorkingDates
            {
                HourFrom = new TimeSpan(5, 0, 0),
                HourTo = new TimeSpan(8, 0, 0),
                NameOfDay = DayOfWeek.Monday.ToString(),
            };
            return workingdates;
        }

        private static WorkingDates GetWorkingDatesThatClashes()
        {
            var workingdates = new WorkingDates
            {
                HourFrom = new TimeSpan(14, 0, 0),
                HourTo = new TimeSpan(18, 0, 0),
                NameOfDay = DayOfWeek.Monday.ToString(),
            };
            return workingdates;
        }


        [TestMethod]
        public void Add_Working_Date()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;

                    var isAdded = unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    Assert.IsTrue(isAdded);
                }
                catch
                {
                    Assert.Fail();
                }
            }
            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.Saleline.DeleteAllOnSubmit(secondContext.Saleline);
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                secondContext.ShoppingCart.DeleteAllOnSubmit(secondContext.ShoppingCart);
                secondContext.OrderTable.DeleteAllOnSubmit(secondContext.OrderTable);
                secondContext.WorkingDates.DeleteAllOnSubmit(secondContext.WorkingDates);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.BookedDates.DeleteAllOnSubmit(secondContext.BookedDates);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Add_Working_Date_That_Clashes_Existing_One_Fails()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesThatClashes();
                    //Assign ID of service offer to working dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;

                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var contextC = new DbTestDataContext();
                    using (var unitOfWork2 = new UnitOfWork(contextC))
                    {
                        var isAdded = unitOfWork2.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                        Assert.IsFalse(isAdded);
                    }


                }
                catch
                {
                    Assert.Fail();
                }
            }
            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.Saleline.DeleteAllOnSubmit(secondContext.Saleline);
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                secondContext.ShoppingCart.DeleteAllOnSubmit(secondContext.ShoppingCart);
                secondContext.OrderTable.DeleteAllOnSubmit(secondContext.OrderTable);
                secondContext.WorkingDates.DeleteAllOnSubmit(secondContext.WorkingDates);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.BookedDates.DeleteAllOnSubmit(secondContext.BookedDates);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Add_Offer_To_A_ShoppingCart()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;

                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    var shoppingcartAdded = unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    Assert.IsTrue(shoppingcartAdded);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Get_Available_Hours_From_For_Existing_Service()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var list = unitOfWork.Orders.GetHoursFrom(service.ID, new DateTime(2018, 12, 10));

                    Assert.AreEqual(11, list.Count);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Get_Available_Hours_From_For_Existing_Service_Which_Has_Already_Booked_Hours()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    unitOfWork.Orders.CreateOrder(user.Logging_ID);

                    var list = unitOfWork.Orders.GetHoursFrom(service.ID, new DateTime(2018, 12, 10));



                    Assert.AreEqual(7, list.Count);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Creation_Of_Order_Returns_Object()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    var result = unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    Assert.IsNotNull(result);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Creation_Of_Order_Set_Order_Status_As_In_Process()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    var result = unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    Assert.AreEqual("In process", result.OrderStatus.Order_status.ToString());
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Cancel_Of_Paid_Order_Returns_False()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    var result = unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    var paid = unitOfWork.Orders.PayForOrder(result);
                    Order o = new Order
                    {
                        ID = paid.ID,
                    };
                    Assert.IsFalse(unitOfWork.Orders.CancelOrder(o));
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Paying_For_Order_Set_Its_Status_As_Completed()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    var result = unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    var paid = unitOfWork.Orders.PayForOrder(result);
                    Assert.AreEqual("Completed", paid.OrderStatus.Order_status.ToString());
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Cancel_Order_Empties_OrderTable_And_Salelines()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    var result = unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    Order o = new Order
                    {
                        ID = result.ID,
                    };
                    unitOfWork.Orders.CancelOrder(o);
                    }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Creation_Of_Two_Orders_With_Same_OrderService_Returns_Exception()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    Assert.Fail();
                }
                catch (BookedTimeException)
                {
                    Assert.IsTrue(true);
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Get_Available_Hours_To_For_Existing_Service()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    //Adds working dates for the offer
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var list = unitOfWork.Orders.GetHoursTo(service.ID, new DateTime(2018, 12, 10), new TimeSpan(17, 0, 0));
                    Assert.AreEqual(3, list.Count);

                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Get_Available_Hours_To_For_Existing_Service_Which_Has_Already_Booked_Hours()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    var workingDatesRequireAddingServiceID2 = GetWorkingDatesSecond();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;
                    workingDatesRequireAddingServiceID2.ServiceOffer_ID = service.ID;
                    //Adds working dates for the offer
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID2);
                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCartLate();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);

                    unitOfWork.Orders.CreateOrder(user.Logging_ID);
                    var list = unitOfWork.Orders.GetHoursTo(service.ID, new DateTime(2018, 12, 10), new TimeSpan(16, 0, 0));
                    Assert.AreEqual(1, list.Count);

                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.Saleline.DeleteAllOnSubmit(context.Saleline);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.ShoppingCart.DeleteAllOnSubmit(context.ShoppingCart);
                    context.OrderTable.DeleteAllOnSubmit(context.OrderTable);
                    context.WorkingDates.DeleteAllOnSubmit(context.WorkingDates);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.BookedDates.DeleteAllOnSubmit(context.BookedDates);
                    context.SubmitChanges();
                }
            }
        }

        [TestMethod]
        public void Add_The_Same_Offer_To_A_ShoppingCart_Should_Fail()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;

                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    var shoppingcartAdded = unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);
                    var sameshoppingcartAdded = unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);
                    Assert.IsFalse(sameshoppingcartAdded);
                }
                catch
                {
                    Assert.Fail();
                }
            }
            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.Saleline.DeleteAllOnSubmit(secondContext.Saleline);
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                secondContext.ShoppingCart.DeleteAllOnSubmit(secondContext.ShoppingCart);
                secondContext.OrderTable.DeleteAllOnSubmit(secondContext.OrderTable);
                secondContext.WorkingDates.DeleteAllOnSubmit(secondContext.WorkingDates);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.BookedDates.DeleteAllOnSubmit(secondContext.BookedDates);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Add_The_Same_Offer_To_A_ShoppingCart_Adds_Only_One()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;

                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    var shoppingcartAdded = unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);
                    var sameshoppingcartAdded = unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);
                    Assert.AreEqual(1, unitOfWork.Orders.GetShoppingCart(user.Logging_ID).Count);
                }
                catch
                {
                    Assert.Fail();
                }
            }
            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.Saleline.DeleteAllOnSubmit(secondContext.Saleline);
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                secondContext.ShoppingCart.DeleteAllOnSubmit(secondContext.ShoppingCart);
                secondContext.OrderTable.DeleteAllOnSubmit(secondContext.OrderTable);
                secondContext.WorkingDates.DeleteAllOnSubmit(secondContext.WorkingDates);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.BookedDates.DeleteAllOnSubmit(secondContext.BookedDates);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Delete_Item_From_Shopping_Cart_Works()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    //Creating users
                    var user = unitOfWork.Users.Create(GetUser());
                    //Creating service
                    var service = unitOfWork.Offers.Create(GetServiceOffer());

                    var workingDatesRequireAddingServiceID = GetWorkingDates();
                    //Assign ID of service offer to woring dates
                    workingDatesRequireAddingServiceID.ServiceOffer_ID = service.ID;

                    unitOfWork.Offers.AddWorkingDates(workingDatesRequireAddingServiceID);

                    var shoppingcartRequireAddingIDs = GetShoppingCart();
                    //Assing ID of service offer to shopping cart
                    shoppingcartRequireAddingIDs.Service_ID = service.ID;
                    //Assing ID of user to shopping cart
                    shoppingcartRequireAddingIDs.User_ID = user.Logging_ID;
                    //Add shopping cart to a database.
                    var shoppingcartAdded = unitOfWork.Orders.AddToCart(shoppingcartRequireAddingIDs);
                    unitOfWork.Orders.DeleteFromCart(shoppingcartRequireAddingIDs);
                    Assert.AreEqual(0, unitOfWork.Orders.GetShoppingCart(user.Logging_ID).Count);
                }
                catch
                {
                    Assert.Fail();
                }
            }
            var secondContext = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.Saleline.DeleteAllOnSubmit(secondContext.Saleline);
                secondContext.ServiceOffer.DeleteAllOnSubmit(secondContext.ServiceOffer);
                secondContext.ShoppingCart.DeleteAllOnSubmit(secondContext.ShoppingCart);
                secondContext.OrderTable.DeleteAllOnSubmit(secondContext.OrderTable);
                secondContext.WorkingDates.DeleteAllOnSubmit(secondContext.WorkingDates);
                secondContext.AspNetUsers.DeleteAllOnSubmit(secondContext.AspNetUsers);
                secondContext.BookedDates.DeleteAllOnSubmit(secondContext.BookedDates);
                secondContext.SubmitChanges();
            }
        }
    }
}