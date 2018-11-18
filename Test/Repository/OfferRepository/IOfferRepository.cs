using Repository.DbConnection;

namespace Repository
{
    public interface IOfferRepository : IRepository<ServiceOffer>
    {
        WorkingDate AddToService(ServiceOffer offer, WorkingDate date);
    }
}
