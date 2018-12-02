using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Linq;

namespace Repository
{
    public interface IOfferRepository : IRepository<ServiceOffer>
    {
        bool AddWorkingDates(WorkingDates days);
        IQueryable<WorkingDates> GetAllWorkingDays();
        IQueryable<Salelines> GetAllBought(string ID);
        bool AddReview(Review review);
        IQueryable<Review> GetServiceReviews(int serviceOfferId);

    }
}
