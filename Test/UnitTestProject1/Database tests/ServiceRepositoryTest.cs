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
            var context = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.Offers.Create(GetServiceOffer());
                Assert.IsTrue(result);
                context.ServiceOffer.DeleteAllOnSubmit(context.ServiceOffer);
            }

           

        }
    }
}
