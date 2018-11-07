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

        public Offer CreateServiceOffer(Offer serviceOffer)
        {


           _database.Create(new ServiceOffer
           {


           })
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