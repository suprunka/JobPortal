using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.DbConnection;
using UnitTestProject1.Database_tests;
using AddressTables = Repository.DbConnection.AddressTable;
using Category = Repository.DbConnection.Category;
using Gender = Repository.DbConnection.Gender;
using AspNetUser = Repository.DbConnection.AspNetUsers;
using ServiceOffer = Repository.DbConnection.ServiceOffer;
using SubCategory = Repository.DbConnection.SubCategory;
using Users = Repository.DbConnection.Users;

namespace UnitTestProject1
{
    //Works
    [TestClass]
    public class ServiceRepositoryTest
    {

        private static Users GetUser()
        {
            var userStub = new Users
            {
                AddressTable = new AddressTables
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
        private static Users SecondUser()
        {
            var userStub = new Users
            {

                AddressTable = new AddressTables
                {
                    Postcode = "9000",
                    City = "Aalborg",
                    Region = "Nordjylland"
                },
                AspNetUsers = new AspNetUser
                {
                    PasswordHash = "Adama1",
                    UserName = "Username123",
                    PhoneNumber = "12345678",
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
                    Category= new Category
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
        private static ServiceOffer GetSecondServiceOffer()
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
                RatePerHour = 40,
                Description = "Sample",
                Employee_ID = "Username12",
                Title = "Second",
            };
            return ServiceOffertub;
        }
        private static ServiceOffer GetThirdServiceOffer()
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
                RatePerHour = 30,
                Description = "Sample",
                Employee_ID = "Username12",
                Title = "Third",
            };
            return ServiceOffertub;
        }
        private static ServiceOffer GetForthServiceOffer()
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
                RatePerHour = 30,
                Description = "Sample",
                Employee_ID = "Username123",
                Title = "Third",
            };
            return ServiceOffertub;
        }
        private static ServiceOffer ToUpdateServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                SubCategory = new SubCategory
                {
                    Name = "Buildings",
                    Category = new Category
                    {
                        Name = "Architecture",
                    },
                },
                RatePerHour = 100,
                Description = "Updated",
                Employee_ID = "Username123",
                Title = "Updated",
            };
            return ServiceOffertub;
        }
        private static ServiceOffer GetInvalidServiceOffer()
        {
            var ServiceOffertub = new ServiceOffer
            {
                SubCategory = new SubCategory
                {
                    Name = "Notexists",
                    Category = new Category
                    {
                        Name = "Notexists",
                    },
                },
                RatePerHour = -1,
                Description = " ",
                Employee_ID = "hftrtoerp[ggpohi54-6934-09-023",
                Title = "Inval",
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
        public void Test_Create_Offer_With_Not_Existing_SubOffer_Throws_Exception_And_Do_Not_Add()
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
                    var list = unitOfWork.Offers.List(t => t.AspNetUsers.PhoneNumber == "12345678");
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
