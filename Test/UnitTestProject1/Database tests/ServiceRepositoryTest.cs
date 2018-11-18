﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.DbConnection;
using UnitTestProject1.Database_tests;
using AddressTable = Repository.DbConnection.AddressTable;
using Category = Repository.DbConnection.Category;
using Gender = Repository.DbConnection.Gender;
using AspNetUser = Repository.DbConnection.AspNetUser;
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
                AspNetUser = new AspNetUser
                {
                    PasswordHash = "Adama1",
                    UserName = "Username1",
                    PhoneNumber = "12345678",
                    Email = "adam@gmail.com",


                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

                FirstName = "Adam",
                LastName = "Adam",
                AddressLine = "mickiewicza",

            };
            return userStub;
        }
        private static Users SecondUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTable
                {
                    Postcode = "8000",
                    City = "Aarhus",
                    Region = "Midtjylland"
                },
                AspNetUser = new AspNetUser
                {
                    PasswordHash = "Adama1",
                    UserName = "Username1",
                    PhoneNumber = "12345678",
                    Email = "adam@gmail.com",


                },
                Gender = new Gender
                {
                    Gender1 = "Male",
                },

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
                Subcategory_ID = 1,
                RatePerHour = 20,
                Description = "Sample",
                Employee_ID = 1,
                Title = "First",
            };
            return ServiceOffertub;
        }
        private static ServiceOffer GetSecondServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                Subcategory_ID = 1,
                RatePerHour = 40,
                Description = "Sample",
                Employee_ID = 1,
                Title = "Second",
            };
            return ServiceOffertub;
        }
        private static ServiceOffer GetThirdServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                Subcategory_ID = 1,
                RatePerHour = 30,
                Description = "Sample",
                Employee_ID = 1,
                Title = "Third",
            };
            return ServiceOffertub;
        }
        private static ServiceOffer GetForthServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                Subcategory_ID = 1,
                RatePerHour = 30,
                Description = "Sample",
                Employee_ID = 12,
                Title = "Third",
            };
            return ServiceOffertub;
        }

        private static ServiceOffer ToUpdateServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                Subcategory_ID = 5,
                RatePerHour = 100,
                Description = "Updated",
                Employee_ID = 12,
                Title = "Updated",
            };
            return ServiceOffertub;
        }

        private static ServiceOffer GetInvalidServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                Subcategory_ID = -5,
                RatePerHour = 20,
                Description = "Sample",
                Employee_ID = 12,
                Title = "Invalid",
            };
            return ServiceOffertub;
        }

        //Testing adding service. [OK]
        [TestMethod]
        public void TestCreationOfOffer()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    var result = unitOfWork.Offers.Create(GetServiceOffer());
                    Assert.IsNotNull(result);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

        //Testing adding service with not existing subcategory ID doesnt add. [OK]
        [TestMethod]
        public void Test_Create_Offer_With_Not_Existing_SubOffer_ID_Throws_Exception_And_Do_Not_Add()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    var result = unitOfWork.Offers.Create(GetInvalidServiceOffer());
                }
                catch (Exception)
                {
                    Assert.IsTrue(true);
                    Assert.AreEqual(0, context.GetTable<ServiceOffer>().Count());
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

        //Testing reading specific service offer by its ID. [OK]
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
                    Assert.AreEqual("Cleaning", created.SubCategory.Name.ToString());
                    Assert.AreEqual(created.SubCategory, result.SubCategory);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

        //Testing getting all available service offers [OK]
        [TestMethod]
        public void Test_Getting_All_Service_Offers()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Offers.Create(GetServiceOffer());
                    unitOfWork.Offers.Create(GetSecondServiceOffer());
                    unitOfWork.Offers.Create(GetThirdServiceOffer());
                    var list = unitOfWork.Offers.GetAll();
                    Assert.AreEqual(3, list.Count());
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

        //Testing filtering list of available services by user phoneNumber. [OK]
        [TestMethod]
        public void Test_Filtering_Using_Phone_Number()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(SecondUser());
                    unitOfWork.Offers.Create(GetServiceOffer());
                    unitOfWork.Offers.Create(GetSecondServiceOffer());
                    unitOfWork.Offers.Create(GetThirdServiceOffer());
                    unitOfWork.Offers.Create(GetForthServiceOffer());
                    var list = unitOfWork.Offers.List(t => t.Users.AspNetUser.PhoneNumber == "35896452");
                    Assert.AreEqual(1, list.Count());
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

        //Testing filtering list of available services by rate by hour. [OK]
        [TestMethod]
        public void Test_Fitering_Using_Rate_By_Hour()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    unitOfWork.Users.Create(SecondUser());
                    unitOfWork.Offers.Create(GetServiceOffer());
                    unitOfWork.Offers.Create(GetSecondServiceOffer());
                    unitOfWork.Offers.Create(GetThirdServiceOffer());
                    unitOfWork.Offers.Create(GetForthServiceOffer());
                    var list = unitOfWork.Offers.List(t => t.RatePerHour < 40);
                    Assert.AreEqual(3, list.Count());
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

        //Testing editing a service offer
        [TestMethod]
        public void Test_Editing_A_Service_Offer()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    ServiceOffer service = unitOfWork.Offers.Create(GetServiceOffer());
                    ServiceOffer toUpdate = ToUpdateServiceOffer();
                    toUpdate.ID = service.ID;
                    var result = unitOfWork.Offers.Update(toUpdate);
                    Assert.IsTrue(result);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

        //Testing deleting a service offer [OK]
        [TestMethod]
        public void Test_Deleting_A_Service_Offer()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetUser());
                    ServiceOffer service = unitOfWork.Offers.Create(GetServiceOffer());
                    var result = unitOfWork.Offers.Delete(t => t.ID == service.ID);
                    Assert.IsTrue(result);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                    context.SubmitChanges();
                }
            }
        }

    }
}
