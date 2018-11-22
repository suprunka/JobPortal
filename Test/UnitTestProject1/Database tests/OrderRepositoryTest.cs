using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;

namespace UnitTestProject1.Database_tests
{
    [TestClass]
    public class OrderRepositoryTest
    {
       
        [TestMethod]
        public void CreateOrderTest()
        {
            var context = new DbTestDataContext();
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                 //   unitOfWork.Orders.CreateOrder(GetUser());
                 // //  var result = unitOfWork.Offers.Create(Ordre());
                 //   Assert.IsNotNull(result);
                }
                catch
                {
                    Assert.Fail();
                }
                finally
                {
                    context.ServiceOffers.DeleteAllOnSubmit(context.ServiceOffers);
                    context.SubmitChanges();
                    context.Users.DeleteAllOnSubmit(context.Users);
                    context.AspNetUsers.DeleteAllOnSubmit(context.AspNetUsers);
                    context.AddressTables.DeleteAllOnSubmit(context.AddressTables);
                    context.SubmitChanges();
                }
            }
        }
    }
}
