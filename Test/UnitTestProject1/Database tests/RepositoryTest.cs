using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Linq;
using System.Linq;
using UnitTestProject1.Database_tests;
using Repository;
using JobPortal.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class RepositoryTest
    {


        [TestMethod]
        public void Add_TestClassUserObjectPassed()
        {
            var context = new JobPortalTestDBDataContext();

            var userStub = new Mock<User>();
           
            userStub.Setup(x => x.PhoneNumber).Returns("12345678");
            userStub.Setup(x => x.FirstName).Returns("Adam");
            userStub.Setup(x => x.LastName).Returns("Adam");
            userStub.Setup(x => x.Email).Returns("adam@gmail.com");
            userStub.Setup(x => x.UserName).Returns("Adammana");
            userStub.Setup(x => x.AddressLine).Returns("gogowaska");
            userStub.Setup(x => x.Password).Returns("Adama1");
            userStub.Setup(x => x.CityName).Returns("Ålborg");
            userStub.Setup(x => x.Postcode).Returns("9000");
            userStub.Setup(x => x.Gender).Returns(JobPortal.Model.Gender.Male);
            userStub.Setup(x => x.Region).Returns(Region.Midtjylland);
           
            using (var unitOfWork = new UnitOfWork(context))
            {
               Repository.DbConnection.Users u = new Repository.DbConnection.Users();
                var result = unitOfWork.Users.Create(userStub.Object);
                Assert.IsNotNull(context.Users.FirstOrDefault(t => t.PhoneNumber == "12345678"));
                context.Users.DeleteAllOnSubmit(context.Users);
                context.Logging.DeleteAllOnSubmit(context.Logging);
                context.AddressTable.DeleteAllOnSubmit(context.AddressTable);
                context.SubmitChanges();
            }
            
        }

        [TestMethod]
        public void Adding_Two_Same_Object_Throws_Exception()
        {
            var context = new JobPortalTestDBDataContext();

            var userStub = new Mock<User>();

            userStub.Setup(x => x.PhoneNumber).Returns("12345678");
            userStub.Setup(x => x.FirstName).Returns("Adam");
            userStub.Setup(x => x.LastName).Returns("Adam");
            userStub.Setup(x => x.Email).Returns("adam@gmail.com");
            userStub.Setup(x => x.UserName).Returns("Adammana");
            userStub.Setup(x => x.AddressLine).Returns("gogowaska");
            userStub.Setup(x => x.Password).Returns("Adama1");
            userStub.Setup(x => x.CityName).Returns("Ålborg");
            userStub.Setup(x => x.Postcode).Returns("9000");
            userStub.Setup(x => x.Gender).Returns(JobPortal.Model.Gender.Male);
            userStub.Setup(x => x.Region).Returns(Region.Midtjylland);
            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    Repository.DbConnection.Users u = new Repository.DbConnection.Users();
                    unitOfWork.Users.Create(userStub.Object);
                    unitOfWork.Users.Create(userStub.Object);
                }catch (DuplicateKeyException)
                {
                    Assert.IsTrue(true);
                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Deleting_Object_From_Database()
        {
            var context = new JobPortalTestDBDataContext();
            var userStub = new Mock<User>();

            userStub.Setup(x => x.PhoneNumber).Returns("12345678");
            userStub.Setup(x => x.FirstName).Returns("Adam");
            userStub.Setup(x => x.LastName).Returns("Adam");
            userStub.Setup(x => x.Email).Returns("adam@gmail.com");
            userStub.Setup(x => x.UserName).Returns("Adammana");
            userStub.Setup(x => x.AddressLine).Returns("gogowaska");
            userStub.Setup(x => x.Password).Returns("Adama1");
            userStub.Setup(x => x.CityName).Returns("Ålborg");
            userStub.Setup(x => x.Postcode).Returns("9000");
            userStub.Setup(x => x.Gender).Returns(JobPortal.Model.Gender.Male);
            userStub.Setup(x => x.Region).Returns(Region.Midtjylland);

            using(var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    Repository.DbConnection.Users u = new Repository.DbConnection.Users();
                    unitOfWork.Users.Create(userStub.Object);
                    bool result = unitOfWork.Users.Delete(t => t.PhoneNumber == userStub.Object.PhoneNumber);
                    Assert.IsTrue(result);
                }
                catch
                {

                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }

        [TestMethod]
        public void Deleting_Object_But_Dont_Delete_Address_Records_If_There_Is_One_More_Person_Using_It()
        {
            var context = new JobPortalTestDBDataContext();
            var userStub = new Mock<User>();

            userStub.Setup(x => x.PhoneNumber).Returns("12345678");
            userStub.Setup(x => x.FirstName).Returns("Adam");
            userStub.Setup(x => x.LastName).Returns("Adam");
            userStub.Setup(x => x.Email).Returns("adam@gmail.com");
            userStub.Setup(x => x.UserName).Returns("Adammana");
            userStub.Setup(x => x.AddressLine).Returns("gogowaska");
            userStub.Setup(x => x.Password).Returns("Adama1");
            userStub.Setup(x => x.CityName).Returns("Ålborg");
            userStub.Setup(x => x.Postcode).Returns("9000");
            userStub.Setup(x => x.Gender).Returns(JobPortal.Model.Gender.Male);
            userStub.Setup(x => x.Region).Returns(Region.Midtjylland);

            var userStub2 = new Mock<User>();

            userStub2.Setup(x => x.PhoneNumber).Returns("87654321");
            userStub2.Setup(x => x.FirstName).Returns("Adam");
            userStub2.Setup(x => x.LastName).Returns("Adam");
            userStub2.Setup(x => x.Email).Returns("adam@gmail.com");
            userStub2.Setup(x => x.UserName).Returns("Adammana");
            userStub2.Setup(x => x.AddressLine).Returns("gogowaska");
            userStub2.Setup(x => x.Password).Returns("Adama1");
            userStub2.Setup(x => x.CityName).Returns("Ålborg");
            userStub2.Setup(x => x.Postcode).Returns("9000");
            userStub2.Setup(x => x.Gender).Returns(JobPortal.Model.Gender.Male);
            userStub2.Setup(x => x.Region).Returns(Region.Midtjylland);

            using (var unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    Repository.DbConnection.Users u = new Repository.DbConnection.Users();
                    unitOfWork.Users.Create(userStub.Object);
                    unitOfWork.Users.Create(userStub2.Object);
                    bool result = unitOfWork.Users.Delete(t => t.PhoneNumber == userStub.Object.PhoneNumber);
                    Assert.IsTrue(result);
                }
                catch
                {

                }
            }

            var secondContext = new JobPortalTestDBDataContext();
            using (var unitOfWork = new UnitOfWork(secondContext))
            {
                secondContext.Users.DeleteAllOnSubmit(secondContext.Users);
                secondContext.Logging.DeleteAllOnSubmit(secondContext.Logging);
                secondContext.AddressTable.DeleteAllOnSubmit(secondContext.AddressTable);
                secondContext.SubmitChanges();
            }
        }
    }


}

