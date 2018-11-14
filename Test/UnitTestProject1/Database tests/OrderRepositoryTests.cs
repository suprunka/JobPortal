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
using JobPortal.Model;

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
            var result = unitOfWork.Orders.CreateOrder(GetUser());
            /*Order o = new Order(new User
            {
                ID = GetUser().ID,
                AddressLine = GetUser().AddressLine,
                CityName = GetUser().AddressTable.City,
                Email = GetUser().Email,
                FirstName = GetUser().FirstName,
                Gender = (JobPortal.Model.Gender)Enum.Parse(typeof(Gender), GetUser().Gender.Gender1),
                LastName = GetUser().LastName,
                Password = GetUser().Logging.Password,
                PhoneNumber = GetUser().PhoneNumber,
                Postcode = GetUser().AddressTable.Postcode,
                Region = (Region)Enum.Parse(typeof(Region), GetUser().AddressTable.Region),
                UserName = GetUser().Logging.UserName,
            });


            o.AddOffer(new Offer {
                Category = (JobPortal.Model.Category)Enum.Parse(typeof(JobPortal.Model.Category), GetServiceOffer().SubCategory.Category.Name),
                Description = GetServiceOffer().Description,
                RatePerHour = GetServiceOffer().RatePerHour,
                Subcategory = (JobPortal.Model.SubCategory)Enum.Parse(typeof(JobPortal.Model.SubCategory),GetServiceOffer().SubCategory.Name),
                Title = GetServiceOffer().Title,
            }, 5);

            o.AddOffer(new Offer
            {
                Category = (JobPortal.Model.Category)Enum.Parse(typeof(JobPortal.Model.Category), GetSecondServiceOffer().SubCategory.Category.Name),
                Description = GetSecondServiceOffer().Description,
                RatePerHour = GetSecondServiceOffer().RatePerHour,
                Subcategory = (JobPortal.Model.SubCategory)Enum.Parse(typeof(JobPortal.Model.SubCategory), GetSecondServiceOffer().SubCategory.Name),
                Title = GetSecondServiceOffer().Title,
            }, 10);*/

            unitOfWork.Orders.AddToExistingOrder(result, serviceOffer, 5);
            unitOfWork.Orders.AddToExistingOrder(result, serviceOffer2, 10);
            
        }


    }

    [TestMethod]
    public void Test_Adding_Items_To_Order()
    {
        User user = new User
        {
            FirstName = "Adam",
            LastName = "Adam",
            AddressLine = "Adamaska",
            CityName = "Adam",
            Email = "elo320",
            Gender = JobPortal.Model.Gender.Male,
            Password = "Qwe1",
            PhoneNumber = "12345678",
            Postcode = "1234",
            Region = Region.Nordjylland,
            UserName = "Siemanko",
        };

        User user2 = new User
        {
            FirstName = "Adam",
            LastName = "Adam",
            AddressLine = "Adamaska",
            CityName = "Adam",
            Email = "elo320",
            Gender = JobPortal.Model.Gender.Male,
            Password = "Qwe1",
            PhoneNumber = "87654321",
            Postcode = "1234",
            Region = Region.Nordjylland,
            UserName = "Siekanko",
        };

        Offer offer1 = new Offer
        {
            Author = user,
            Category = JobPortal.Model.Category.Home,
            Description = "Description",
            RatePerHour = 20,
            Subcategory = JobPortal.Model.SubCategory.Babysitting,
            Title = "Title",
        };

        Offer offer2 = new Offer
        {
            Author = user,
            Category = JobPortal.Model.Category.Media,
            Description = "Description",
            RatePerHour = 10,
            Subcategory = JobPortal.Model.SubCategory.Design,
            Title = "Tytuł",
        };

        Order o = new Order(user2);
        o.AddOffer(offer1, 5);
        o.AddOffer(offer2, 6);
    }
}

