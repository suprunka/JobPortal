using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JobPortal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Repository.DbConnection;
using ServiceLibrary;
using Gender = Repository.DbConnection.Gender;

namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class ServiceOfferServiceTests
    {
        //Create
        #region

        [TestMethod]
        public void Create_OfferService_Verify_If_It_Calls_Db()
        {
            try
            {
                var offerServiceMock = new Mock<Offer>();
                offerServiceMock.Setup(x => x.Author).Returns(new User
                {
                    ID = 1,
                    PhoneNumber = "12345678",
                    FirstName = "Adam",
                    LastName = "Adam",
                    Email = "adam@gmail.com",
                    UserName = "Username1",
                    Password = "Adama1",
                    AddressLine = "mickiewicza",
                    CityName = "Aalborg",
                    Postcode = "9000",
                    Region = Region.Nordjylland,
                    Gender = JobPortal.Model.Gender.Male,
                });
                offerServiceMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerServiceMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerServiceMock.Setup(x => x.RatePerHour).Returns(20);
                offerServiceMock.Setup(x => x.Title).Returns("Title");
                offerServiceMock.Setup(x => x.Description).Returns("Description");
                var dbMock = new Mock<IOfferRepository>();
                //works fine even without the line below
                /*dbMock.Setup(x => x.Create(It.IsAny<ServiceOffer>())).Returns(new ServiceOffer
                {
                    ID = 1,
                    Users = new Repository.DbConnection.Users
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

                    },
                    Employee_Phone = "12345678",
                    Description = "Descriptionlong",
                    Title = "Title",
                    RatePerHour = 20,
                    SubCategory = new Repository.DbConnection.SubCategory
                    {
                        Category = new Repository.DbConnection.Category
                        {
                            ID = 1,
                            Name = "Home",
                        },
                        ID = 1,
                        Category_ID = 1,
                        Name = "Cleaning",
                    },
                    Subcategory_ID = 1,
                });*/
                OfferService service = new OfferService(dbMock.Object);
                service.CreateServiceOffer(offerServiceMock.Object);
                dbMock.Verify(x => x.Create(It.IsAny<ServiceOffer>()), Times.AtLeastOnce);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Create_OfferService_Check_If_Is_True()
        {
            try
            {
                var offerServiceMock = new Mock<Offer>();
                offerServiceMock.Setup(x => x.Author).Returns(new User
                {
                    ID = 1,
                    PhoneNumber = "12345678",
                    FirstName = "Adam",
                    LastName = "Adam",
                    Email = "adam@gmail.com",
                    UserName = "Username1",
                    Password = "Adama1",
                    AddressLine = "mickiewicza",
                    CityName = "Aalborg",
                    Postcode = "9000",
                    Region = Region.Nordjylland,
                    Gender = JobPortal.Model.Gender.Male,
                });
                offerServiceMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerServiceMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerServiceMock.Setup(x => x.RatePerHour).Returns(20);
                offerServiceMock.Setup(x => x.Title).Returns("Title");
                offerServiceMock.Setup(x => x.Description).Returns("Description");

                var dbMock = new Mock<IOfferRepository>();
                /*dbMock.Setup(x => x.Create(It.IsAny<ServiceOffer>())).Returns(new ServiceOffer
                {
                    ID = 1,
                    Users = new Repository.DbConnection.Users
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

                    },
                    Employee_Phone = "12345678",
                    Description = "Descriptionlong",
                    Title = "Title",
                    RatePerHour = 20,
                    SubCategory = new Repository.DbConnection.SubCategory
                    {
                        Category = new Repository.DbConnection.Category
                        {
                            ID = 1,
                            Name = "Home",
                        },
                        ID = 1,
                        Category_ID = 1,
                        Name = "Cleaning",
                    },
                    Subcategory_ID = 1,
                });*/

                OfferService service = new OfferService(dbMock.Object);
                var created = service.CreateServiceOffer(offerServiceMock.Object);
                Assert.IsTrue(created);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        #region
        [DataRow(-1, "My Title 10", "This is description..20", DisplayName = "Test with rate per hour<0 and another valid data")]
        [DataRow(0, "My Title 10", "This is description..20", DisplayName = "Test with rate per hour == 0 and another valid data")]
        [DataRow(1, "", "This is description..20", DisplayName = "Test with empty title and another valid data")]
        [DataRow(1, "My Title 10", "", DisplayName = "Test with empty description and another valid data")]
        [DataRow(1, "My Title 10", "Descri", DisplayName = "Test with decsription<20 letters and another valid data")]
        [DataRow(1, "10", "This is description..20", DisplayName = "Test with title<10 letters and another valid data")]

        #endregion
        public void Create_OfferService_With_Invalid_Inputs_Never_Calls_Database(int ratePerHour, string title, string description)
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Author).Returns(new User
            {
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                UserName = "Username1",
                Password = "Adama1",
                AddressLine = "mickiewicza",
                CityName = "Aalborg",
                Postcode = "9000",
                Region = Region.Nordjylland,
                Gender = JobPortal.Model.Gender.Male,
            });
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
            offerMock.Setup(x => x.RatePerHour).Returns(ratePerHour);
            offerMock.Setup(x => x.Title).Returns(title);
            offerMock.Setup(x => x.Description).Returns(description);
            var dbMock = new Mock<IOfferRepository>();
            var sut = new OfferService(dbMock.Object);
            sut.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<ServiceOffer>()), Times.Never);
        }
        #endregion

        //Delete 
        #region
        [TestMethod]
        public void Delete_OfferService_Verify_If_It_Calls_Db()
        {
            var offerMock = new Mock<ServiceOffer>();
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IOfferRepository>();

            var sut = new OfferService(dbMock.Object);
            sut.DeleteServiceOffer(offerMock.Object.ID);
            dbMock.Verify(x => x.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>()), Times.AtLeastOnce);

        }
        [TestMethod]
        public void Delete_OfferService_Verify_If_Returns_true()
        {
            var offerMock = new Mock<Offer>();
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IOfferRepository>();

            dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(true);

            var sut = new OfferService(dbMock.Object);
            var result = sut.DeleteServiceOffer(offerMock.Object.Id);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Delete_OfferService_Using_Invalid_ID_Returns_False()
        {
            var offerMock = new Mock<Offer>();
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IOfferRepository>();

            dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(true);

            var sut = new OfferService(dbMock.Object);
            var result = sut.DeleteServiceOffer(-1);
            Assert.IsFalse(result);
        }
        
        #endregion
        

        //Read
        #region
        [TestMethod]
        public void Get_OfferService_Verify_If_It_Calls_Db()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IOfferRepository>();

            dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
            {
                ID = 1,
                Users = new Repository.DbConnection.Users
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

                },
                Employee_ID = 1,
                Description = "Descriptionlong",
                Title = "Title",
                RatePerHour = 20,
                SubCategory = new Repository.DbConnection.SubCategory
                {
                    Category = new Repository.DbConnection.Category
                    {
                        ID = 1,
                        Name = "Home",
                    },
                    ID = 1,
                    Category_ID = 1,
                    Name = "Cleaning",
                },
                Subcategory_ID = 1,
            });

            var sut = new OfferService(dbMock.Object);
            sut.FindServiceOffer(offerMock.Object.Id);
            dbMock.Verify(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>()), Times.Once());
        }

        [TestMethod]
        public void GetAll_OfferService_Verify_If_Returns_Queryable()
        {
            var dbMock = new Mock<IOfferRepository>();

            dbMock.Setup(x => x.GetAll()).Returns(new List<ServiceOffer>(){ new ServiceOffer
            {
                ID = 1,
                Users = new Repository.DbConnection.Users
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

                },
                Employee_ID = 1,
                Description = "Descriptionlong",
                Title = "Title",
                RatePerHour = 20,
                SubCategory = new Repository.DbConnection.SubCategory
                {
                    Category = new Repository.DbConnection.Category
                    {
                        ID = 1,
                        Name = "Home",
                    },
                    ID = 1,
                    Category_ID = 1,
                    Name = "Cleaning",
                },
                Subcategory_ID = 1,
            } , new ServiceOffer{
                 ID = 1,
                Users = new Repository.DbConnection.Users
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

                },
                Employee_ID = 1,
                Description = "Descriptionlong",
                Title = "Title",
                RatePerHour = 20,
                SubCategory = new Repository.DbConnection.SubCategory
                {
                    Category = new Repository.DbConnection.Category
                    {
                        ID = 1,
                        Name = "Home",
                    },
                    ID = 1,
                    Category_ID = 1,
                    Name = "Cleaning",
                },
                Subcategory_ID = 1,
            }
            }.AsQueryable());
        
            var sut = new OfferService(dbMock.Object);
            var foundOffer = sut.GetAllOffers();
            Assert.AreEqual(2, foundOffer.Count());
        }

        [TestMethod]
        public void Get_OfferService_Returns_Offer()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IOfferRepository>();

            dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
            {
                ID = 1,
                Users = new Repository.DbConnection.Users
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

                },
                Employee_ID = 1,
                Description = "Descriptonlong",
                Title = "Title",
                RatePerHour = 20,
                SubCategory = new Repository.DbConnection.SubCategory
                {
                    Category = new Repository.DbConnection.Category
                    {
                        ID = 1,
                        Name = "Home",
                    },
                    ID = 1,
                    Category_ID = 1,
                    Name = "Cleaning",
                },
                Subcategory_ID = 1,
            });

            var sut = new OfferService(dbMock.Object);
            var foundOffer = sut.FindServiceOffer(offerMock.Object.Id);

            Assert.IsNotNull(foundOffer);
        }

        [TestMethod]
        public void Get_Test_Proprties_Of_Found_Offer()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IOfferRepository>();

            dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
            {
                ID = 1,
                Users = new Repository.DbConnection.Users
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

                },
                Employee_ID = 1,
                Description = "Descriptionlong",
                Title = "Title",
                RatePerHour = 20,
                SubCategory = new Repository.DbConnection.SubCategory
                {
                    Category = new Repository.DbConnection.Category
                    {
                        ID = 1,
                        Name = "Home",
                    },
                    ID = 1,
                    Category_ID = 1,
                    Name = "Cleaning",
                },
                Subcategory_ID = 1,
            });

            var sut = new OfferService(dbMock.Object);
            var foundOffer = sut.FindServiceOffer(offerMock.Object.Id);

            Assert.AreEqual(20, foundOffer.RatePerHour);
        }



        #endregion

        //Update
        #region
        [TestMethod]
        public void Update_OfferService_Verify_If_It_Calls_Db()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Author).Returns(new User
            {
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                UserName = "Username1",
                Password = "Adama1",
                AddressLine = "mickiewicza",
                CityName = "Aalborg",
                Postcode = "9000",
                Region = Region.Nordjylland,
                Gender = JobPortal.Model.Gender.Male,
            });
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
            offerMock.Setup(x => x.RatePerHour).Returns(20);
            offerMock.Setup(x => x.Title).Returns("Title");
            offerMock.Setup(x => x.Description).Returns("Description");
            var dbMock = new Mock<IOfferRepository>();
            var sut = new OfferService(dbMock.Object);
            sut.UpdateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Update(It.IsAny<ServiceOffer>()), Times.Once());

        }

        [TestMethod]
        public void Update_OfferService_With_Invalid_Title_Is_False()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Author).Returns(new User
            {
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                UserName = "Username1",
                Password = "Adama1",
                AddressLine = "mickiewicza",
                CityName = "Aalborg",
                Postcode = "9000",
                Region = Region.Nordjylland,
                Gender = JobPortal.Model.Gender.Male,
            });
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
            offerMock.Setup(x => x.RatePerHour).Returns(20);
            offerMock.Setup(x => x.Title).Returns("T");
            offerMock.Setup(x => x.Description).Returns("Description");
            var dbMock = new Mock<IOfferRepository>();
            var sut = new OfferService(dbMock.Object);
            var result = sut.UpdateServiceOffer(offerMock.Object);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Update_OfferService_With_Invalid_Description_Is_False()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Author).Returns(new User
            {
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                UserName = "Username1",
                Password = "Adama1",
                AddressLine = "mickiewicza",
                CityName = "Aalborg",
                Postcode = "9000",
                Region = Region.Nordjylland,
                Gender = JobPortal.Model.Gender.Male,
            });
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
            offerMock.Setup(x => x.RatePerHour).Returns(20);
            offerMock.Setup(x => x.Title).Returns("Title");
            offerMock.Setup(x => x.Description).Returns("Short");
            var dbMock = new Mock<IOfferRepository>();
            var sut = new OfferService(dbMock.Object);
            var result = sut.UpdateServiceOffer(offerMock.Object);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Update_OfferService_With_Invalid_Rate_Per_Hour_Is_False()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Author).Returns(new User
            {
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                UserName = "Username1",
                Password = "Adama1",
                AddressLine = "mickiewicza",
                CityName = "Aalborg",
                Postcode = "9000",
                Region = Region.Nordjylland,
                Gender = JobPortal.Model.Gender.Male,
            });
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
            offerMock.Setup(x => x.RatePerHour).Returns(-10);
            offerMock.Setup(x => x.Title).Returns("Title");
            offerMock.Setup(x => x.Description).Returns("Description");
            var dbMock = new Mock<IOfferRepository>();
            var sut = new OfferService(dbMock.Object);
            var result = sut.UpdateServiceOffer(offerMock.Object);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Update_OfferService_Verify_If_Returns_Valid_Object()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Author).Returns(new User
            {
                ID = 1,
                PhoneNumber = "12345678",
                FirstName = "Adam",
                LastName = "Adam",
                Email = "adam@gmail.com",
                UserName = "Username1",
                Password = "Adama1",
                AddressLine = "mickiewicza",
                CityName = "Aalborg",
                Postcode = "9000",
                Region = Region.Nordjylland,
                Gender = JobPortal.Model.Gender.Male,
            });
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
            offerMock.Setup(x => x.RatePerHour).Returns(20);
            offerMock.Setup(x => x.Title).Returns("Title");
            offerMock.Setup(x => x.Description).Returns("Description");

            var dbMock = new Mock<IOfferRepository>();
            var sut = new OfferService(dbMock.Object);
            var result = sut.UpdateServiceOffer(offerMock.Object);
            Assert.IsTrue(result);
        }


        #endregion


        

    }
}

