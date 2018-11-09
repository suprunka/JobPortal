using System;
using System.Linq;
using AppJobPortal.Model;
using System.ServiceModel;
using Repository.DbConnection;
using Repository;
using ServiceLibrary.Models;

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
            throw new NotImplementedException();
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
                if ((RegexMatch.DoesOfferMatch(serviceOffer)) && (serviceOffer.RatePerHour > 0))
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

        public IQueryable<Offer> GetAllOffers()
        {
            throw new NotImplementedException();
        }

        
    }
}