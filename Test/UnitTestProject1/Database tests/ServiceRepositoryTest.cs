using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.DbConnection;
using UnitTestProject1.Database_tests;
using AddressTable = Repository.DbConnection.AddressTable;
using Category = Repository.DbConnection.Category;
using Gender = Repository.DbConnection.Gender;
using Logging = Repository.DbConnection.Logging;
using ServiceOffer = Repository.DbConnection.ServiceOffer;
using SubCategory = Repository.DbConnection.SubCategory;
using Users = Repository.DbConnection.Users;

namespace UnitTestProject1
{
    [TestClass]
    public class ServiceRepositoryTest
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
                Logging = new Logging
                {
                    Password = "Adama1",
                    UserName = "Username1",
                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                AddressLine = "mickiewicza",

            };
            return userStub;
        }
        private static ServiceOffer GetServiceOffer()
        {
            var serviceOfferStub = new ServiceOffer
            {
                SubCategory = new SubCategory
                {
                    ID = 1,
                    Name = "House",
                    Category = new Category
                    {
                        ID = 1,
                        Name = "Cleaning",
                    }
                },
                RatePerHour = 20,
                Description = "Sample",
                Employee_Phone = "12345678",
                Title = "Sample",
            };
            return serviceOfferStub;
        }
        private static ServiceOffer GetInvalidServiceOffer()
        {
            var serviceOfferStub = new ServiceOffer
            {
                SubCategory = new SubCategory
                {
                    ID = -1,
                    Name = "House",
                    Category = new Category
                    {
                        ID = -1,
                        Name = "Cleaning",
                    }
                },
                RatePerHour = 20,
                Description = "Sample",
                Employee_Phone = "12345678",
                Title = "Sample",
            };
            return serviceOfferStub;
        }

        //Testing adding service
        [TestMethod]
        public void TestCreationOfOffer()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var result = unitOfWork.Offers.Create(GetServiceOffer());
                    Assert.IsNotNull(result);
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                }
                catch
                {
                    Assert.Fail();
                }
                
            }
        }

        //Testing adding service with not existing subcategory ID
        [TestMethod]
        public void Test_Create_Offer_With_Not_Existing_ID()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var result = unitOfWork.Offers.Create(GetInvalidServiceOffer());
                }
                catch (Exception e)
                {
                    Assert.IsTrue(true);
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                }
            }
        }

        [TestMethod]
        public void Test_Getting_Specific_Service_Offer()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    ServiceOffer created = unitOfWork.Offers.Create(GetServiceOffer());
                    ServiceOffer result = unitOfWork.Offers.Get(t => t.ID == created.ID);
                    Assert.AreEqual(created.Title, result.Title);
                }
                catch 
                {
                    Assert.Fail();
                }
            }
        }

    }
}
