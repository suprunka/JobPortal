using System;
using System.Linq;
using Repositories;
using ServiceLibrary.Models;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
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