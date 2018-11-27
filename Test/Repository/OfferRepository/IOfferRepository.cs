using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Linq;

namespace Repository
{
    public interface IOfferRepository : IRepository<ServiceOffer>
    {
        bool AddWorkingDates(Days day, TimeSpan hourFrom, TimeSpan hourTo, ServiceOffer s);
        IQueryable<ServiceOffer> GetAllBought(string ID);
    }
}
