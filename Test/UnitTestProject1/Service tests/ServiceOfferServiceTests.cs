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


namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class ServiceOfferServiceTests
    {
        //Create

        [TestMethod]
        public void Create_OfferService_Verify_If_It_Calls_Db()
        {
            try
            {
                var offerServiceMock = new Mock<Offer>();
                offerServiceMock.Setup(x => x.AuthorId).Returns("1");
                offerServiceMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerServiceMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerServiceMock.Setup(x => x.RatePerHour).Returns(20);
                offerServiceMock.Setup(x => x.Title).Returns("Title");
                offerServiceMock.Setup(x => x.Description).Returns("Description");
                var dbMock = new Mock<IOfferRepository>();
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
                offerServiceMock.Setup(x => x.AuthorId).Returns("1");
                offerServiceMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerServiceMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerServiceMock.Setup(x => x.RatePerHour).Returns(20);
                offerServiceMock.Setup(x => x.Title).Returns("Title");
                offerServiceMock.Setup(x => x.Description).Returns("Description");
                var dbMock = new Mock<IOfferRepository>();
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
        [DataRow(-1, "My Title 10", "This is description..20", DisplayName = "Test with rate per hour<0 and another valid data")]
        [DataRow(0, "My Title 10", "This is description..20", DisplayName = "Test with rate per hour == 0 and another valid data")]
        [DataRow(1, "", "This is description..20", DisplayName = "Test with empty title and another valid data")]
        [DataRow(1, "My Title 10", "", DisplayName = "Test with empty description and another valid data")]
        [DataRow(1, "My Title 10", "Descri", DisplayName = "Test with decsription<20 letters and another valid data")]
        [DataRow(1, "10", "This is description..20", DisplayName = "Test with title<10 letters and another valid data")]
        public void Create_OfferService_With_Invalid_Inputs_Never_Calls_Database(int ratePerHour, string title, string description)
        {
            try
            {
                var offerMock = new Mock<Offer>();
                offerMock.Setup(x => x.AuthorId).Returns("1");
                offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerMock.Setup(x => x.RatePerHour).Returns(ratePerHour);
                offerMock.Setup(x => x.Title).Returns(title);
                offerMock.Setup(x => x.Description).Returns(description);
                var dbMock = new Mock<IOfferRepository>();
                var subject = new OfferService(dbMock.Object);
                subject.CreateServiceOffer(offerMock.Object);
                dbMock.Verify(x => x.Create(It.IsAny<ServiceOffer>()), Times.Never);
            }
            catch
            {
                Assert.Fail();
            }
        }


        //Delete 

        [TestMethod]
        public void Delete_OfferService_Verify_If_It_Calls_Db()
        {
            try
            {
                var dbMock = new Mock<IUnitOfWork>();
                var subject = new OfferService(dbMock.Object);
                subject.DeleteServiceOffer(1);
                dbMock.Verify(x => x.Offers.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>()), Times.AtLeastOnce);
            }
            catch
            {
                Assert.Fail();
            }


        }

        [TestMethod]
        public void Delete_OfferService_Verify_If_Returns_true()
        {
            try
            {
                var dbMock = new Mock<IOfferRepository>();
                dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(true);
                var subject = new OfferService(dbMock.Object);
                var result = subject.DeleteServiceOffer(1);
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Delete_OfferService_Using_Invalid_ID_Returns_False()
        {
            try
            {
                var dbMock = new Mock<IOfferRepository>();
                dbMock.Setup(t => t.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(true);
                var subject = new OfferService(dbMock.Object);
                var result = subject.DeleteServiceOffer(-1);
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
        }

        //Read

        [TestMethod]
        public void Get_OfferService_Verify_If_It_Calls_Db()
        {
            try
            {
                var offerMock = new Mock<Offer>();
                offerMock.Setup(x => x.Id).Returns(1);
                var dbMock = new Mock<IOfferRepository>();

                dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
                {
                    ID = 1,
                    Employee_ID = "1",
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

                var subject = new OfferService(dbMock.Object);
                subject.FindServiceOffer(offerMock.Object.Id);
                dbMock.Verify(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>()), Times.Once());
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetAll_OfferService_Verify_If_Returns_Queryable()
        {
            try
            {
                var dbMock = new Mock<IOfferRepository>();
                dbMock.Setup(x => x.GetAll()).Returns(new List<ServiceOffer>(){ new ServiceOffer
            {
                ID = 1,
                Employee_ID = "1",
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
            }, new ServiceOffer
            {
                ID = 1,
                Employee_ID = "1",
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
                var subject = new OfferService(dbMock.Object);
                var foundOffers = subject.GetAllOffers();
                Assert.AreEqual(2, foundOffers.Count());
            }
            catch
            {
                Assert.Fail();
            }
      
        }

        [TestMethod]
        public void Get_OfferService_Returns_Offer()
        {
            try
            {
                var dbMock = new Mock<IOfferRepository>();
                dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
                {
                    ID = 1,
                    Employee_ID = "1",
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
                var subject = new OfferService(dbMock.Object);
                var foundOffer = subject.FindServiceOffer(1);
                Assert.IsNotNull(foundOffer);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Get_OfferService_Compare_Attributes()
        {
            try
            {
                var dbMock = new Mock<IOfferRepository>();
                dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
                {
                    ID = 1,
                    Employee_ID = "1",
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
                var subject = new OfferService(dbMock.Object);
                var foundOffer = subject.FindServiceOffer(1);
                Assert.AreEqual("Descriptonlong", foundOffer.Description);
            }
            catch
            {
                Assert.Fail();
            }
            
        }

        [TestMethod]
        public void Get_Test_Proprties_Of_Found_Offer()
        {
            try
            {
                var dbMock = new Mock<IOfferRepository>();
                dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
                {
                    ID = 1,
                    Employee_ID = "1",
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

                var subject = new OfferService(dbMock.Object);
                var foundOffer = subject.FindServiceOffer(1);
                Assert.AreEqual("Home", foundOffer.Category.ToString());
            }
            catch
            {
                Assert.Fail();
            }
           
        }


        //Update
        [TestMethod]
        public void Update_OfferService_Verify_If_It_Calls_Db()
        {
            try
            {
                var offerMock = new Mock<Offer>();
                offerMock.Setup(x => x.AuthorId).Returns("1");
                offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerMock.Setup(x => x.RatePerHour).Returns(20);
                offerMock.Setup(x => x.Title).Returns("Title");
                offerMock.Setup(x => x.Description).Returns("Description");
                var dbMock = new Mock<IOfferRepository>();
                var subject = new OfferService(dbMock.Object);
                subject.UpdateServiceOffer(offerMock.Object);
                dbMock.Verify(x => x.Update(It.IsAny<ServiceOffer>()), Times.Once());
            }
            catch
            {
                Assert.Fail();
            }
       

        }

        [TestMethod]
        public void Update_OfferService_With_Invalid_Title_Is_False()
        {
            try
            {
                var offerMock = new Mock<Offer>();
                offerMock.Setup(x => x.AuthorId).Returns("1");
                offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerMock.Setup(x => x.RatePerHour).Returns(20);
                offerMock.Setup(x => x.Title).Returns("T");
                offerMock.Setup(x => x.Description).Returns("Description");
                var dbMock = new Mock<IOfferRepository>();
                var subject = new OfferService(dbMock.Object);
                var result = subject.UpdateServiceOffer(offerMock.Object);
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
            

        }

        [TestMethod]
        public void Update_OfferService_With_Invalid_Description_Is_False()
        {
            try
            {
                var offerMock = new Mock<Offer>();
                offerMock.Setup(x => x.AuthorId).Returns("1");
                offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerMock.Setup(x => x.RatePerHour).Returns(20);
                offerMock.Setup(x => x.Title).Returns("Title");
                offerMock.Setup(x => x.Description).Returns("Short");
                var dbMock = new Mock<IOfferRepository>();
                var subject = new OfferService(dbMock.Object);
                var result = subject.UpdateServiceOffer(offerMock.Object);
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
           

        }

        [TestMethod]
        public void Update_OfferService_With_Invalid_Rate_Per_Hour_Is_False()
        {
            try
            {
                var offerMock = new Mock<Offer>();
                offerMock.Setup(x => x.AuthorId).Returns("1");
                offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerMock.Setup(x => x.RatePerHour).Returns(-10);
                offerMock.Setup(x => x.Title).Returns("Title");
                offerMock.Setup(x => x.Description).Returns("Description");
                var dbMock = new Mock<IOfferRepository>();
                var subject = new OfferService(dbMock.Object);
                var result = subject.UpdateServiceOffer(offerMock.Object);
                Assert.IsFalse(result);
            }
            catch
            {
                Assert.Fail();
            }
            
        }

        [TestMethod]
        public void Update_OfferService_Verify_If_Returns_True()
        {
            try
            {
                var offerMock = new Mock<Offer>();
                offerMock.Setup(x => x.AuthorId).Returns("1");
                offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.Home);
                offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.Babysitting);
                offerMock.Setup(x => x.RatePerHour).Returns(20);
                offerMock.Setup(x => x.Title).Returns("Title");
                offerMock.Setup(x => x.Description).Returns("Description");
                var dbMock = new Mock<IOfferRepository>();
                var subject = new OfferService(dbMock.Object);
                var result = subject.UpdateServiceOffer(offerMock.Object);
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }
           
        }
    }
}

