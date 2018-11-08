using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.DbConnection;
using UnitTestProject1.Database_tests;
using ServiceOffer = Repository.DbConnection.ServiceOffer;
using SubCategory = Repository.DbConnection.SubCategory;

namespace UnitTestProject1
{
    [TestClass]
    public class ServiceRepositoryTest
    {

        private static ServiceOffer GetServiceOffer()
        {
            var serviceOfferStub = new ServiceOffer
            {
                SubCategory = new SubCategory
                {
                    ID = 1,
                    Name = "House",
                    Category_ID = 1,
                },
                RatePerHour = 20,
                Description = "Sample",
                Employee_Phone = "123456780",
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
                var result = unitOfWork.Offers.Create(GetServiceOffer());
                Assert.IsTrue(result);
                context.ServiceOffers.DeleteAllOnSubmit(context.ServiceOffers);
            }
        }

        //deleting service
        [TestMethod]
        public void Test_Delete_Of_Service()
        {
            var context =  new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.Offers.Create(GetServiceOffer());
                var result = unitOfWork.Offers.Delete(x => x.ID == GetServiceOffer().ID);
                Assert.IsTrue(result);
                context.ServiceOffers.DeleteAllOnSubmit(context.ServiceOffers);

            }
        }

      
    }
}
