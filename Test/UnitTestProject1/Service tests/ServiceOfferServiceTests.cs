using System;
using System.Linq;
using System.Linq.Expressions;
using JobPortal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Repository.DbConnection;
using ServiceLibrary;
using UnitTestProject1.Database_tests;
using ServiceOffer = Repository.DbConnection.ServiceOffer;
using Users = UnitTestProject1.Database_tests.Users;

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
            var offerServiceMock = new Mock<ServiceOffer>();
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            offerMock.Setup(x => x.RatePerHour).Returns(220);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.AppPrgramming);
            offerMock.Setup(x => x.Title).Returns("Title123456789");
            offerMock.Setup(x => x.Author).Returns(new Mock<User>().SetupAllProperties().Object);
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.IT);
            offerMock.Setup(x => x.Description).Returns("verylongdescription verylongdescription verylongdescription");


            var dbMock = new Mock<IOfferRepository>();

            OfferService service = new OfferService(dbMock.Object);
            service.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<ServiceOffer>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void Create_OfferService_Check_The_Return_Object()
        {
            var offerMock = new Mock<ServiceOffer>();
            var offer2Mock = new Mock<Offer>();
            var userMock = new Mock<User>();
            userMock.SetupAllProperties();
            offer2Mock.Setup(x => x.Author).Returns(userMock.Object);
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IOfferRepository>();

            var sut = new OfferService(dbMock.Object);

            ServiceOffer OfferSentToDb = null;
            offer2Mock.Setup(x => x.Id).Returns(offerMock.Object.ID);
            

            dbMock.Setup(x => x.Create(It.IsAny<ServiceOffer>())).Returns(offerMock.Object)
                .Callback<ServiceOffer>(x => x = OfferSentToDb);

            sut.CreateServiceOffer(offer2Mock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<ServiceOffer>()), Times.Once());
            Assert.AreEqual(1, OfferSentToDb.ID);
        }

        [TestMethod]
        [DataRow(-1, "My Title >10", "This is description..>20", DisplayName = "Test with rate per hour<0 and another valid data")]
        [DataRow(0, "My Title >10", "This is description..>20", DisplayName = "Test with rate per hour == 0 and another valid data")]
        [DataRow(1, "", "This is description..>20", DisplayName = "Test with empty title and another valid data")]
        [DataRow(1, "My Title >10", "", DisplayName = "Test with empty description and another valid data")]
        [DataRow(1, "My Title >10", "Description<20", DisplayName = "Test with decsription<20 letters and another valid data")]
        [DataRow(1, "<10", "This is description..>20", DisplayName = "Test with title<10 letters and another valid data")]
        [DataRow(1, "My Title >10", "This is description..>20", DisplayName = "Test with all valid data ")]

        public void Create_OfferService_With_Specific_Inputs(int ratePerHour, string title, string description)
        {
            var offerMock = new Mock<Offer>();

            offerMock.Setup(x => x.RatePerHour).Returns(ratePerHour);
            offerMock.Setup(x => x.Title).Returns(title);
            offerMock.Setup(x => x.Description).Returns(description);
            var dbMock = new Mock<IOfferRepository>();

            var sut = new OfferService(dbMock.Object);
            sut.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<ServiceOffer>()), Times.AtLeastOnce);
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
           
            var dbMock = new Mock<IOfferRepository>();

            bool result =false;
            dbMock.Setup(x => x.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(true);

            var sut = new OfferService(dbMock.Object);
            try
            {
              //  sut.CreateServiceOffer(offerMock.Object);
                 result = sut.DeleteServiceOffer(6);
                
            }
            catch {
                result = false;
            }
            Assert.IsTrue(result);

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
            var serviceOfferMock = new Mock<ServiceOffer>();

        ///    dbMock.Setup(x=>x.D)
            dbMock.Setup(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>())).Returns(new ServiceOffer());
         //  {
         //      ID =1, Description="ddddddd", Employee_Phone="0000111",
         //      RatePerHour =222, Subcategory_ID =1, Title="dddd", Users = new Repository.DbConnection.Users
         //      {
         //          AddressTable = new Repository.DbConnection.AddressTable
         //          {
         //              Postcode = "9000",
         //              City = "Aalborg",
         //              Region = "Nordjylland"
         //          },
         //          Logging = new Repository.DbConnection.Logging
         //          {
         //              Password = "Adama1",
         //              UserName = "Username1",
         //          },
         //          Gender = new Repository.DbConnection.Gender
         //          {
         //              Gender1 = "Male",
         //          },
         //          ID = 1,
         //          PhoneNumber = "0000111",
         //          FirstName = "Adam",
         //          LastName = "Adam",
         //          Email = "adam@gmail.com",
         //          AddressLine = "mickiewicza",
         //
         //      }
         //     
         //  });
            var sut = new OfferService(dbMock.Object);
            var returnedOffer = sut.FindServiceOffer(1);
            dbMock.Verify(x => x.Get(It.IsAny<Expression<Func<ServiceOffer, bool>>>()), Times.Once());
            Assert.IsNotNull( returnedOffer);
        }
        [TestMethod]
        public void GetAll_OfferService_Verify_If_Returns_Queryable()
        {
            var dbMock = new Mock<IOfferRepository>();
            IQueryable<Offer> list = null;
            dbMock.Setup(x => x.GetAll()).Returns(new ServiceOffer[] { new ServiceOffer() }.AsQueryable<ServiceOffer>())
                .Callback<IQueryable<Offer>>(x => list = x);
            var sut = new OfferService(dbMock.Object);
            sut.GetAllOffers();
            dbMock.Verify(x => x.GetAll(), Times.Once());

            Assert.AreEqual(1, list.Count());
        }

        #endregion

        //Update
        #region
        [TestMethod]
        public void Update_OfferService_Verify_If_Connect_To_Db()
        {
            var offerMock = new Mock<Offer>();
            var dbMock = new Mock<IOfferRepository>();
            offerMock.Setup(x => x.Id).Returns(1);
            offerMock.Setup(x => x.RatePerHour).Returns(220);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.AppPrgramming);
            offerMock.Setup(x => x.Title).Returns("Title123456789");
            offerMock.Setup(x => x.Author).Returns(new Mock<User>().SetupAllProperties().Object);
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.IT);
            offerMock.Setup(x => x.Description).Returns("verylongdescription verylongdescription verylongdescription");
            var sut = new OfferService(dbMock.Object);
            sut.UpdateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Update(It.IsAny<ServiceOffer>()), Times.Once());

        }

        [TestMethod]
        public void Update_OfferService_Verify_If_Returns_Valid_Object()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            offerMock.Setup(x => x.RatePerHour).Returns(220);
            offerMock.Setup(x => x.Subcategory).Returns(JobPortal.Model.SubCategory.AppPrgramming);
            offerMock.Setup(x => x.Title).Returns("Title123456789");
            offerMock.Setup(x => x.Author).Returns(new Mock<User>().SetupAllProperties().Object);
            offerMock.Setup(x => x.Category).Returns(JobPortal.Model.Category.IT);
            offerMock.Setup(x => x.Description).Returns("verylongdescription verylongdescription verylongdescription");

            var dbMock = new Mock<IOfferRepository>();
            dbMock.Setup(x => x.Update(It.IsAny<ServiceOffer>()));

            var sut = new OfferService(dbMock.Object);
            bool res = sut.UpdateServiceOffer(offerMock.Object);
            Assert.IsTrue(res);
        }
       /* [TestMethod]
        public void Edit_With_Valid_Inputs()
        {
            var userMock = new Mock<UserWebModel>();
            userMock.Setup(x => x.LastName).Returns("Dreuk");
            userMock.Setup(x => x.PhoneNumber).Returns("12334455");
            var userServiceStub = new Mock<IUserService>();
            Users userSenftoService = null;

            userServiceStub.Setup(x => x.EditUser(It.IsAny<Users>()))
                .Callback<Users>(x => userSenftoService = x);

            var sut = new UserController(userServiceStub.Object);
            sut.Edit(userMock.Object);
            userServiceStub.Verify(x =>
           x.EditUser(It.IsAny<Users>()), Times.Once());

            Assert.IsTrue(
                userSenftoService.LastName == "Dreuk" &&
                userSenftoService.PhoneNumber == "12334455"
                );
        }*/
       #endregion
        
    }
}

