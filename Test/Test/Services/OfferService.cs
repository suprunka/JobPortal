using System;
using System.Linq;
using Repositories;
using System.ServiceModel.Description;
using ServiceLibrary.Models;
using AppJobPortal.Model;
using System.ServiceModel;
using Repository.OfferRepository;
using Repository.DbConnection;

namespace ServiceLibrary
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _database;

        public OfferService(IRepository<Offer> database)
        {
            _database = new OfferRepository(new JobPortalDatabaseDataContext());
        }

        public OfferService()
        {

        }

        public bool CreateServiceOffer(Offer offer)
        {

            _database.Create(new Repository.DbConnection.ServiceOffer
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
            throw new NotImplementedException();
        }

        public bool UpdateServiceOffer(Offer serviceOffer)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Offer> GetAllOffers()
        {
            throw new NotImplementedException();
        }

        
    }
}