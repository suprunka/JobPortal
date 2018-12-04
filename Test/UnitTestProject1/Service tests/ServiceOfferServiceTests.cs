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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(t => t.Offers.Create(It.IsAny<ServiceOffer>()));
                OfferService service = new OfferService(dbMock.Object);
                service.CreateServiceOffer(offerServiceMock.Object);
                dbMock.Verify(x => x.Offers.Create(It.IsAny<ServiceOffer>()), Times.AtLeastOnce);
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(t => t.Offers.Create(It.IsAny<ServiceOffer>()));
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
                var dbMock = new Mock<IUnitOfWork>();
                var subject = new OfferService(dbMock.Object);
                subject.CreateServiceOffer(offerMock.Object);
                dbMock.Verify(x => x.Offers.Create(It.IsAny<ServiceOffer>()), Times.Never);
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
                dbMock.Setup(x => x.Offers.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>()));
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(t => t.Offers.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(true);
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(t => t.Offers.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(true);
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
                var dbMock = new Mock<IUnitOfWork>();

                dbMock.Setup(x => x.Offers.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
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
                dbMock.Verify(x => x.Offers.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>()), Times.Once());
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetAll()).Returns(new List<ServiceOffer>(){ new ServiceOffer
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer
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


        //UpdateServiceOffer
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.Update(It.IsAny<ServiceOffer>()));
                var subject = new OfferService(dbMock.Object);
                subject.UpdateServiceOffer(offerMock.Object);
                dbMock.Verify(x => x.Offers.Update(It.IsAny<ServiceOffer>()), Times.Once());
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
                var dbMock = new Mock<IUnitOfWork>();
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
                var dbMock = new Mock<IUnitOfWork>();
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
                var dbMock = new Mock<IUnitOfWork>();
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
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.Update(It.IsAny<ServiceOffer>()));
                var subject = new OfferService(dbMock.Object);
                var result = subject.UpdateServiceOffer(offerMock.Object);
                Assert.IsTrue(result);
            }
            catch
            {
                Assert.Fail();
            }

        }
        
        //AddHoursToOffer
        [TestMethod]
        public void Test_Add_Hours_To_Offer_Returns_True()
        {
            try
            {
                var workingTimeMock = new Mock<WorkingTime>();
                workingTimeMock.SetupAllProperties();
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.AddWorkingDates(It.IsAny<WorkingDates>())).Returns(true);
                var subject = new OfferService(dbMock.Object);
                Assert.IsTrue(subject.AddHoursToOffer(workingTimeMock.Object));
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test_Add_Hours_To_Offer_Returns_False()
        {
            try
            {
                var workingTimeMock = new Mock<WorkingTime>();
                workingTimeMock.SetupAllProperties();
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.AddWorkingDates(It.IsAny<WorkingDates>())).Returns(false);
                var subject = new OfferService(dbMock.Object);
                Assert.IsFalse(subject.AddHoursToOffer(workingTimeMock.Object));
            }
            catch
            {
                Assert.Fail();
            }
        }
       
        //GetAllWorkingDays
        [TestMethod]
        public void Test_Get_All_Working_Days_Returns_TypeOf_IQuaryable()
        {
            try
            {

                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetAllWorkingDays()).Returns(new List<WorkingDates>() {
                new WorkingDates {
                    ID = 1,
                    HourFrom = new TimeSpan(3, 0, 0),
                    HourTo = new TimeSpan(5,0,0),
                    NameOfDay = DayOfWeek.Friday.ToString(),
                    ServiceOffer_ID = 3,
                }, new WorkingDates
                {
                    ID = 2,
                    HourFrom = new TimeSpan(5, 0, 0),
                    HourTo = new TimeSpan(10, 0, 0),
                    NameOfDay = DayOfWeek.Sunday.ToString(),
                    ServiceOffer_ID = 10,
                }}.AsQueryable());
                var subject = new OfferService(dbMock.Object);
                Assert.IsInstanceOfType(subject.GetAllWorkingDays(), typeof(IQueryable<WorkingTime>));
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test_Get_All_Working_Days_Returns_Empty_IQuaryable()
        {
            try
            {

                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetAllWorkingDays()).Returns(new List<WorkingDates>().AsQueryable());
                var subject = new OfferService(dbMock.Object);
                Assert.AreEqual(0, subject.GetAllWorkingDays().Count());
            }
            catch
            {
                Assert.Fail();
            }
        }
        
        //GetAllBought
        [TestMethod]
        public void Test_Get_All_Bought_Services_Of_User()
        {
            try
            {

                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetAllBought(It.IsAny<string>())).Returns(new List<Salelines>() {
                new Salelines {
                    ServiceOffer_ID = 3,
                    ServiceOffer = new ServiceOffer
                    {
                        Description ="dsadasdasd",
                        Title = "sdsadas",
                        RatePerHour = 32,
                        SubCategory = new Repository.DbConnection.SubCategory
                        {
                            Category = new Repository.DbConnection.Category
                            {
                                Name = "Home",
                            },
                            Name= "Cleaning",
                        },
                    },
                    BookedDate = new BookedDate
                    {
                        BookedDate1 =  new DateTime(2018,12,21),
                        HourFrom = new TimeSpan(13,0,0),
                        HourTo = new TimeSpan(17,0,0),
                    }
                }}.AsQueryable());
                var subject = new OfferService(dbMock.Object);
                Assert.AreEqual(1, subject.GetAllBought("12").Count());
            }
            catch
            {
                Assert.Fail();
            }
        }
        
        //AddReview
        [TestMethod]
        public void Test_Adding_Review_Above_Scale_Returns_False()
        {
            try
            {
                var reviewMock = new Mock<OfferReview>();
                reviewMock.Setup(x => x.Rate).Returns(500);
                reviewMock.Setup(x => x.Comment).Returns("dsa");
                var dbMock = new Mock<IUnitOfWork>();                
                var subject = new OfferService(dbMock.Object);
                Assert.IsFalse(subject.AddReview(reviewMock.Object));
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test_Adding_Too_Long_Comment_Returns_False()
        {
            try
            {
                var reviewMock = new Mock<OfferReview>();
                reviewMock.Setup(x => x.Rate).Returns(3);
                reviewMock.Setup(x => x.Comment).Returns("dsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsaddsadsadsad");
                var dbMock = new Mock<IUnitOfWork>();
                var subject = new OfferService(dbMock.Object);
                Assert.IsFalse(subject.AddReview(reviewMock.Object));
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test_Adding_Valid_Comment_Returns_True()
        {
            try
            {
                var reviewMock = new Mock<OfferReview>();
                reviewMock.Setup(x => x.Rate).Returns(3);
                reviewMock.Setup(x => x.Comment).Returns("Good");
                reviewMock.Setup(x => x.CustomerId).Returns("23");
                reviewMock.Setup(x => x.ServiceOfferId).Returns(2);

                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.AddReview(It.IsAny<Review>())).Returns(true);
                var subject = new OfferService(dbMock.Object);
                Assert.IsTrue(subject.AddReview(reviewMock.Object));
            }
            catch
            {
                Assert.Fail();
            }
        }
        
        //GetServiceReviews
        [TestMethod]
        public void Test_Getting_Service_Reviews_Returns_IQuaryable()
        {
            try
            {
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetServiceReviews(It.IsAny<int>())).Returns(new List<Review>()
                { new Review
                {
                    Customer_ID = "121",
                    Comment = "zxcasd",
                    RateValue = 3,
                    ServiceOffer_ID = 21

                }, new Review
                {
                     Customer_ID = "121",
                    Comment = "zxcasd",
                    RateValue = 3,
                    ServiceOffer_ID = 21
                }
                    
                }.AsQueryable());
                var subject = new OfferService(dbMock.Object);
                Assert.IsInstanceOfType(subject.GetServiceReviews(21), typeof(IQueryable<OfferReview>));
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test_Getting_Service_Reviews_Returns_Null()
        {
            try
            {
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetServiceReviews(It.IsAny<int>())).Returns(new List<Review>().AsQueryable());
                var subject = new OfferService(dbMock.Object);
                Assert.IsNull(subject.GetServiceReviews(21));
            }
            catch
            {
                Assert.Fail();
            }
        }

        //GetAvgOfServiceRates
        [TestMethod]
        public void Test_Calculating_Average()
        {
            try
            {
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetServiceReviews(It.IsAny<int>())).Returns(new List<Review>()
                { new Review
                {
                    Customer_ID = "121",
                    Comment = "zxcasd",
                    RateValue = 3,
                    ServiceOffer_ID = 21

                }, new Review
                {
                     Customer_ID = "121",
                    Comment = "zxcasd",
                    RateValue = 3,
                    ServiceOffer_ID = 21
                }

                }.AsQueryable());
                var subject = new OfferService(dbMock.Object);
                Assert.AreEqual(3, subject.GetAvgOfServiceRates(21));
            }
            catch
            {
                Assert.Fail();
            }

        }
        [TestMethod]
        public void Test_Calculating_Average2()
        {
            try
            {
                var dbMock = new Mock<IUnitOfWork>();
                dbMock.Setup(x => x.Offers.GetServiceReviews(It.IsAny<int>())).Returns(new List<Review>().AsQueryable());
                var subject = new OfferService(dbMock.Object);
                //throws exception because getavgofservicerates can't calculate avg of null
                subject.GetAvgOfServiceRates(21);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}

