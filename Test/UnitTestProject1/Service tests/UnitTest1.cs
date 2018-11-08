using System;
using System.Linq.Expressions;
using AppJobPortal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;
using Repository.DbConnection;
using Repository.OfferRepository;
using ServiceLibrary;

namespace UnitTestProject1.Service_tests
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void Delete_OfferService_Verify_If_It_Calls_Db()
        {
            var dbMock = new Mock<IOfferRepository>();

            var sut = new OfferService(dbMock.Object);
            sut.DeleteServiceOffer(4);
            dbMock.Verify(x => x.Delete(It.IsAny<Expression<Func<ServiceOffer, bool>>>()), Times.AtLeastOnce);
        }
        [TestMethod]
        public void Delete_OfferService_Verify_If_It_Removes()
        {
            var offerMock = new Mock<ServiceOffer>();
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IOfferRepository>();
            dbMock.Setup(x => x.Create(offerMock.Object));

            var sut = new OfferService(dbMock.Object);
            bool result = sut.DeleteServiceOffer(offerMock.Object.ID);
            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void Delete_OfferService_Verify_If_It_Removes()
        {
            var offerMock = new Mock<ServiceOffer>();
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IOfferRepository>();
            dbMock.Setup(x => x.Create(offerMock.Object));

            var sut = new OfferService(dbMock.Object);
            bool result = sut.DeleteServiceOffer(offerMock.Object.ID);
            Assert.IsTrue(result == true);
        }

        /* [TestMethod]
         public void Get_OfferService_Verify_If_It_Calls_Db()
         {
             var offerMock = new Mock<Offer>();
             offerMock.Setup(x => x.Id).Returns(1);
             var dbMock = new Mock<IRepository<Offer>>();
             int OfferId = -1;

             dbMock.Setup(x => x.Get(It.IsAny<int>()))
                 .Callback<int>(x => OfferId = x);

             var sut = new OfferService(dbMock.Object);
             sut.FindServiceOffer(offerMock.Object.Id);
             dbMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
             Assert.AreEqual(1, OfferId);
         }
         [TestMethod]
         public void GetAll_OfferService_Verify_If_Returns_Queryable()
         {
             var dbMock = new Mock<IRepository<Offer>>();
             IQueryable<Offer> list = null;
             dbMock.Setup(x => x.GetAll()).Returns(new Offer[] { new Offer() }.AsQueryable<Offer>())
                 .Callback<IQueryable<Offer>>(x => list = x);
             var sut = new OfferService(dbMock.Object);
             sut.GetAllOffers();
             dbMock.Verify(x => x.GetAll(), Times.Once());

             Assert.AreEqual(1, list.Count());
         }

         [TestMethod]
         public void Update_OfferService_Verify_If_Connect_To_Db()
         {
             var offerMock = new Mock<Offer>();
             var dbMock = new Mock<IRepository<Offer>>();
             var sut = new OfferService(dbMock.Object);
             sut.UpdateServiceOffer(offerMock.Object);
             dbMock.Verify(x => x.Update(It.IsAny<Offer>()), Times.Once());

         }

         [TestMethod]
         public void Update_OfferService_Verify_If_Returns_Valid_Object()
         {
             var offerMock = new Mock<Offer>();
             offerMock.Setup(x => x.Id).Returns(1);
             offerMock.Setup(x => x.RatePerHour).Returns(100);
             var dbMock = new Mock<IRepository<Offer>>();
             Offer returnedOffer = null;
             dbMock.Setup(x => x.Update(It.IsAny<Offer>()))
                 .Callback<Offer>(x => returnedOffer = x);
             var sut = new OfferService(dbMock.Object);
             sut.UpdateServiceOffer(offerMock.Object);
             Assert.IsTrue(
                 1 == offerMock.Object.Id &&
                 100 == offerMock.Object.RatePerHour
                  );
         }
         [TestMethod]
         public void Edit_With_Valid_Inputs()
         {
             var userMock = new Mock<UserWebModel>();
             userMock.Setup(x => x.LastName).Returns("Dreuk");
             userMock.Setup(x => x.PhoneNumber).Returns("12334455");
             var userServiceStub = new Mock<IUserService>();
             User userSenftoService = null;

             userServiceStub.Setup(x => x.EditUser(It.IsAny<User>()))
                 .Callback<User>(x => userSenftoService = x);

             var sut = new UserController(userServiceStub.Object);
             sut.Edit(userMock.Object);
             userServiceStub.Verify(x =>
            x.EditUser(It.IsAny<User>()), Times.Once());

             Assert.IsTrue(
                 userSenftoService.LastName == "Dreuk" &&
                 userSenftoService.PhoneNumber == "12334455"
                 );
         }
         */
    }
}
