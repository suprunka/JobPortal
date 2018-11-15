using JobPortal.Model;
using Repository.DbConnection;
using System;

namespace Repository
{
    public interface IOfferRepository : IRepository<ServiceOffer>
    {
        bool AddWorkingDates(Days day, TimeSpan hourFrom, TimeSpan hourTo, ServiceOffer s);
    }
}
