using System;
using Repositories;
using ServiceLibrary;
using Moq;
using ServiceLibrary.Models;
using ServiceLibrary.DbConnection;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var offer = new gsggs();
            var context = new Mock<DatabaseDataContext>();
            //var dbTable = new Mock<dupa<Offer>>();
        }
    }
}
