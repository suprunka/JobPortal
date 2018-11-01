using System;
using System.Linq;
using Repositories;
using ServiceLibrary.Models;

namespace ServiceLibrary
{ 
    public class OfferService : IOfferService
    {
        private readonly IRepository<Offer> _database;
        public OfferService(IRepository<Offer> database)
        {
            _database = database;
        }

        public Offer CreateServiceOffer(Offer serviceOffer)
        {
            throw new NotImplementedException();
        }

        public bool DeleteServiceOffer(int ID)
        {
            throw new NotImplementedException();
        }

        public Offer FindServiceOffer(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Offer> GetAllOffers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateServiceOffer(Offer serviceOffer)
        {
            throw new NotImplementedException();
        }
    }
}