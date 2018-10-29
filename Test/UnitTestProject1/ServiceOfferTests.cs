using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;
using ServiceLibrary;
using ServiceLibrary.Models;
namespace UnitTestProject1
{
    [TestClass]
    public class ServiceOfferTests
    {
        [TestMethod]
        public void Test_Create_OfferService_Verify_If_It_Calls_Db()
        {
            var offerMock = new Mock<Offer>();
            offerMock.SetupAllProperties();

            var dbMock = new Mock<IRepository<Offer>>();

            OfferService service = new OfferService(dbMock.Object);

            service.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<Offer>()), Times.AtLeastOnce);
        }
        [TestMethod]
        public void Test_Create_OfferService_()
        {
            var offerMock = new Mock<Offer>();
            offerMock.Setup(x => x.Id).Returns(1);

            var dbMock = new Mock<IRepository<Offer>>();

            Offer receivedOffer = null;

            dbMock.Setup(x => x.Create(It.IsAny<Offer>()))
                .Callback<Offer>(x => x= receivedOffer);
            var sut = new OfferService(dbMock.Object);
            sut.CreateServiceOffer(offerMock.Object);
            dbMock.Verify(x => x.Create(It.IsAny<Offer>()), Times.Once());


        }
    }
}
