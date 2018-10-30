﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;
using ServiceLibrary;
using ServiceLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class ServiceOfferTests
    {
        [TestMethod]
        public void Create_OfferService_Verify_If_It_Calls_Db()
        {
            var offerMock = new Mock<Offer>();
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IRepository<Offer>>();

            OfferService service = new OfferService(dbMock.Object);
            service.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<Offer>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void Create_OfferService_Check_The_Return_Object()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IRepository<Offer>>();
            Offer OfferSentToDb = null;

            dbMock.Setup(x => x.Create(It.IsAny<Offer>()))
                .Callback<Offer>(x => x= OfferSentToDb);

            var sut = new OfferService(dbMock.Object);
            sut.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<Offer>()), Times.Once());
            Assert.AreEqual(1, OfferSentToDb.Id );
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
            var dbMock = new Mock<IRepository<Offer>>();

            var sut = new OfferService(dbMock.Object);
            sut.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<Offer>()), Times.AtLeastOnce);
        }


        [TestMethod]
        public void Delete_OfferService_Verify_If_It_Calls_Db()
        {
            var offerMock = new Mock<Offer>();
            offerMock.SetupAllProperties();
            var dbMock = new Mock<IRepository<Offer>>();

            var sut = new OfferService(dbMock.Object);
            sut.DeleteServiceOffer(offerMock.Object.Id);
            dbMock.Verify(x => x.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void Get_OfferService_Verify_If_It_Calls_Db()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IRepository<Offer>>();
            int OfferId = -1;

            dbMock.Setup(x => x.Get(It.IsAny<int>()))
                .Callback<int>(x => x = OfferId);

            var sut = new OfferService(dbMock.Object);
            sut.FindServiceOffer(offerMock.Object.Id);
            dbMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(1, OfferId);
        }
        [TestMethod]
        public void GetAll_OfferService_Verify_If_Returns_Queryable()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);
            var dbMock = new Mock<IRepository<Offer>>();
            IQueryable<Offer> list = null;
            dbMock.Setup(x => x.GetAll()).Returns(new Offer[] {new Offer() }.AsQueryable<Offer>())
                .Callback<IQueryable<Offer>>(x=> list = x );
            var sut = new OfferService(dbMock.Object);
            dbMock.Verify(x => x.GetAll(), Times.Once());

            Assert.AreEqual(1, list.Count());
        }

    }
}