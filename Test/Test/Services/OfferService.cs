using System;
using System.Linq;
using System.ServiceModel;
using Repository.DbConnection;
using Repository;
using ServiceLibrary.Models;
using System.Collections.Generic;
using JobPortal.Model;
using SubCategory = JobPortal.Model.SubCategory;
using Category = JobPortal.Model.Category;

namespace ServiceLibrary
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _database;

        public OfferService(IOfferRepository database)
        {
            _database = database;
        }

        public OfferService()
        {
            _database = new OfferRepository(new JobPortalDatabaseDataContext());

        }

        public bool CreateServiceOffer(Offer offer)
        {

            _database.Create(new ServiceOffer
            {

                SubCategory = new Repository.DbConnection.SubCategory
                {
                    Name = offer.Subcategory.ToString(),
                    Category = new Repository.DbConnection.Category
                    {
                        Name = offer.Category.ToString(),
                   },
                },
                Title = offer.Title,
                Description = offer.Description,
                RatePerHour = offer.RatePerHour,
                Employee_Phone = offer.Author.ID.ToString()

            });
            return true;
        }

        public Offer FindServiceOffer(int ID)
        {
            UserService userService = new UserService();
            var result =  _database.Get(t => t.ID == ID);
            return new Offer
            {
                Id = result.ID,
                RatePerHour = result.RatePerHour,
                Title = result.Title,
                Description = result.Description,
                Author = userService.FindUser(result.Employee_Phone),
                Subcategory = (SubCategory)Enum.Parse(typeof(SubCategory), result.SubCategory.Name),
                Category = (Category)Enum.Parse(typeof(Category), result.SubCategory.Category.Name),
            };
        }

        public bool DeleteServiceOffer(int ID)
        {
            try
            {
                if (ID > -1)
                {
                   return _database.Delete(t => t.ID == ID);
                   
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }

        }

        public bool UpdateServiceOffer(Offer serviceOffer)
        {
            try
            {
                if (RegexMatch.DoesOfferMatch(serviceOffer) && (serviceOffer.RatePerHour > 0))
                {
                    _database.Update(new ServiceOffer
                    {
                        ID = serviceOffer.Id,
                        Description = serviceOffer.Description,
                        Title = serviceOffer.Title,
                        RatePerHour = serviceOffer.RatePerHour,
                        SubCategory = new Repository.DbConnection.SubCategory
                        {
                            Name = serviceOffer.Subcategory.ToString(),
                            Category = new Repository.DbConnection.Category
                            {
                                Name = serviceOffer.Category.ToString()
                            }
                        }
                    });
                    return true;
                    }
                return false;

            }

            catch
            {
                return false;

            }


        }

        public Offer[] GetAllOffers()
        {
            UserService userService = new UserService();
            IList<Offer> resultToReturn = new List<Offer>();
            foreach(var o in _database.GetAll())
            {
                resultToReturn.Add(new Offer
                {
                    Id = o.ID,
                    RatePerHour = o.RatePerHour,
                    Title = o.Title,
                    Description = o.Description,
                    Author = userService.FindUser(o.Employee_Phone),
                    Subcategory = (SubCategory)Enum.Parse(typeof(SubCategory), o.SubCategory.Name),
                    Category = (Category)Enum.Parse(typeof(Category), o.SubCategory.Category.Name),
                });
            }
            return resultToReturn.ToArray();
        }

        
    }
}