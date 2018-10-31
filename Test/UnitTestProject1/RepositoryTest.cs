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
            var offer = new Offer();
            var context = new Mock<ServiceLibrary.DbConnection.DatabaseDataContext>();
            var dbTable? = new Mock<DatabaseDataContextSet>
        }
    }
}
