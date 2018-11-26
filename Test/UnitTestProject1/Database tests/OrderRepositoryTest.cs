using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DbConnection;

namespace UnitTestProject1.Database_tests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        private static Repository.DbConnection.Users GetUser()
        {
            var userStub = new Repository.DbConnection.Users
            {
                AddressTable = new Repository.DbConnection.AddressTable
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                AspNetUsers = new Repository.DbConnection.AspNetUsers
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
                Gender = new Repository.DbConnection.Gender
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
        private static Repository.DbConnection.ServiceOffer GetServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                SubCategory = new Repository.DbConnection.SubCategory
                {
                    Name = "Cleaning",
                    Category = new Repository.DbConnection.Category
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

        [TestMethod]
        public void CreateOrderTest()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new Repository.UnitOfWork(context))
            {
                try
                {
                    var user = unitOfWork.Users.Create(GetUser());
                    var service = unitOfWork.Offers.Create(GetServiceOffer());
                    var y = unitOfWork.Orders.CreateOrder(user, new List<KeyValuePair<ServiceOffer, JobPortal.Model.BookedDate>>() {
                                    new KeyValuePair<ServiceOffer, JobPortal.Model.BookedDate>(GetServiceOffer(), new JobPortal.Model.BookedDate{ Day= DateTime.Now, HoursFrom = new TimeSpan(10, 30, 0), HoursTo = new TimeSpan(12, 00, 0) }) });

                    //Assert.IsNotNull(result);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffers.DeleteAllOnSubmit(context.ServiceOffers);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTables.DeleteAllOnSubmit(context.AddressTables);
                    context.SubmitChanges();
                }
            }
        }
    }
}
