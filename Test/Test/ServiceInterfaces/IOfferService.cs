using System.Linq;
using System.ServiceModel;
using JobPortal.Model;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IOfferService
    {
        [OperationContract]
        bool CreateServiceOffer(Offer offer);

        [OperationContract]
        Offer FindServiceOffer(int ID);

        [OperationContract]
        bool DeleteServiceOffer(int ID);

        [OperationContract]
        bool UpdateServiceOffer(Offer serviceOffer);

        [OperationContract]
        IQueryable<Offer> GetAllOffers();

        [OperationContract]
        bool AddHoursToOffer(WorkingTime time);

        [OperationContract]
        IQueryable<WorkingTime> GetAllWorkingDays();

        [OperationContract]
        IQueryable<Offer> GetAllBought(string ID);
        [OperationContract]
        bool AddReview(OfferReview review);
        [OperationContract]
        IQueryable<OfferReview> GetServiceReviews(int serviceId);
        [OperationContract]
        double GetAvgOfServiceRates(int serviceId);
    }
}
