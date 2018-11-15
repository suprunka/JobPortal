using System;
using JobPortal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using AddressTable = Repository.DbConnection.AddressTable;
using Users = Repository.DbConnection.Users;
using Logging = Repository.DbConnection.Logging;
using Gender = Repository.DbConnection.Gender;
using UnitTestProject1.Database_tests;
using ServiceOffer = Repository.DbConnection.ServiceOffer;


[TestClass]
public class OrderRepositoryTests
{
    private static Users GetUser()
    {
        var userStub = new Users
        {
            AddressTable = new AddressTable
            {
                Postcode = "9000",
                City = "Aalborg",
                Region = "Nordjylland"
            },
            Logging = new Logging
            {
                Password = "Adama1",
                UserName = "Username1",
            },
            Gender = new Gender
            {
                Gender1 = "Male",
            },

            PhoneNumber = "12345678",
            FirstName = "Adam",
            LastName = "Adam",
            Email = "adam@gmail.com",
            AddressLine = "mickiewicza",
            BankAccountNumber = "255",

        };
        return userStub;

    }
    private static Users GetSecondUser()
    {
        var userStub = new Users
        {
            AddressTable = new AddressTable
            {
                Postcode = "9000",
                City = "Aalborg",
                Region = "Nordjylland"
            },
            Logging = new Logging
            {
                Password = "Adama1",
                UserName = "Username2",
            },
            Gender = new Gender
            {
                Gender1 = "Male",
            },

            PhoneNumber = "87654321",
            FirstName = "Adam",
            LastName = "Adam",
            Email = "adam@gmail.com",
            AddressLine = "mickiewicza",
            BankAccountNumber = "255",

        };
        return userStub;

    }
    private static ServiceOffer GetServiceOffer()
    {
        var serviceOfferStub = new ServiceOffer
        {
            Subcategory_ID = 1,
            RatePerHour = 20,
            Description = "Sample",
            Employee_Phone = "12345678",
            Title = "First",
        };
        return serviceOfferStub;
    }
    private static ServiceOffer GetSecondServiceOffer()
    {
        var serviceOfferStub = new ServiceOffer
        {
            Subcategory_ID = 1,
            RatePerHour = 40,
            Description = "Sample",
            Employee_Phone = "12345678",
            Title = "Second",
        };
        return serviceOfferStub;
    }
    private static ServiceOffer GetThirdServiceOffer()
    {
        var serviceOfferStub = new ServiceOffer
        {
            Subcategory_ID = 1,
            RatePerHour = 30,
            Description = "Sample",
            Employee_Phone = "12345678",
            Title = "Third",
        };
        return serviceOfferStub;
    }

    private static WorkingDates GetWorkingDatesOfServiceOffer()
    {
        var workingdates = new WorkingDates
        {
            NameOfDay = "Monday",
            HourFrom = new TimeSpan(15, 0, 0),
            HourTo = new TimeSpan(18, 0, 0),
        };
        return workingdates;
    }

    [TestMethod]
    public void Test_Creation_Of_Order()
    {
        var context = new DbTestDataContext();
        using (var unitOfWork = new UnitOfWork(context))
        {
            unitOfWork.Users.Create(GetUser());
            unitOfWork.Orders.CreateOrder(GetUser());
        }
    }

    [TestMethod]
    public void Test_Adding_Salelines_To_Existing_Order()
    {
        var context = new DbTestDataContext();
        using (var unitOfWork = new UnitOfWork(context))
        {
            unitOfWork.Users.Create(GetUser());
            unitOfWork.Users.Create(GetSecondUser());
            var serviceOffer = unitOfWork.Offers.Create(GetServiceOffer());
            var serviceOffer2 = unitOfWork.Offers.Create(GetSecondServiceOffer());
            var serviceOffer3 = unitOfWork.Offers.Create(GetThirdServiceOffer());
            unitOfWork.Offers.AddWorkingDates(Days.Saturday, new TimeSpan(12, 0, 0), new TimeSpan(20, 0, 0), serviceOffer);
            var result = unitOfWork.Orders.CreateOrder(GetUser());
            unitOfWork.Orders.AddToExistingOrder(result, serviceOffer, DateTime.Now.AddDays(2), new TimeSpan(11, 0, 0), new TimeSpan(15, 0, 0));
            unitOfWork.Orders.AddToExistingOrder(result, serviceOffer, DateTime.Now.AddDays(9), new TimeSpan(13, 0, 0), new TimeSpan(18, 0, 0));
            //
            unitOfWork.Orders.DeleteFromExistingOrder(result, serviceOffer, DateTime.Now.AddDays(9), new TimeSpan(13, 0, 0), new TimeSpan(18, 0, 0));
            //
            unitOfWork.Orders.PayForOrder(result);

        }
    }
}

