using Repository.OrderRepository;
using System;


namespace Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }

        IOfferRepository Offers { get; }

        IOrderRepository Orders { get; }
        
    }
}
