using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.DbConnection;

namespace UnitTestProject1.Database_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Deleting_Offer_From_Database()
        {
            var context = new TestDbDataContext();


            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.Users.Create(GetOffers());
                    bool result = unitOfWork.Users.Delete(t => t.PhoneNumber == GetUser().PhoneNumber);
                    Assert.IsTrue(result);

                }
                catch
                {
                    Assert.Fail();
                }
            }

        }

        private ServiceOffer GetOffers()
        {
            new ServiceOffer
            {
                SubCategory = new SubCategory
                {
                    Name = "sub1",
                    Category = new Category
                    {

                    }
                    }

                }


            };
            return ;

        }
    }
