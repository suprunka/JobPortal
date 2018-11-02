using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;
using Repository.DbConnection;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using UnitTestProject1;
using Gender = ServiceLibrary.Models.Gender;

namespace UnitTestProject1
{
 
    public  class TestClassUser
    {

        public virtual int ID { get; set; }

        public virtual String PhoneNumber { get; set; }

        public virtual String FirstName { get; set; }

        public virtual String LastName { get; set; }

        public virtual String Email { get; set; }

        public virtual String UserName { get; set; }


        public virtual String Password { get; set; }


        public virtual String AddressLine { get; set; }

        public virtual String CityName { get; set; }

        public virtual String Postcode { get; set; }

        public virtual Region Region { get; set; }

        public virtual Gender Gender { get; set; }
    }


    [TestClass]
    public class RepositoryTest
    {


        /*[TestMethod]
        public void Add_TestClassUserObjectPassed()
        {
            var testObject = new TestClassUser();
            var context = new JobPortalDatabaseDataContext();

            var dbTable = new Table<TestClassUser>();
            var dbSetMock = new Mock<ITable<TestClassUser>>();
            /*context.Setup(x => x.GetTable<TestClassUser>()).Returns(dbSetMock.Object as Table<TestClassUser>);
            dbSetMock.Setup(x => x.InsertOnSubmit(It.IsAny<TestClassUser>()));

            var repository = new Repository<TestClassUser>(context.Object);
            repository.Create(testObject);

            context.Verify(x => x.GetTable<TestClassUser>());

        }*/
    }


}

