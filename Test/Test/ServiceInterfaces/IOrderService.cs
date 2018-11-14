using JobPortal.Model;
using Repository.DbConnection;

namespace ServiceLibrary.ServiceInterfaces
{
    public interface IOrderService
    {
        Order CreateOrder(Users u);

        bool CancelOrder(Order o);

        Order AddToExistingOrder(Offer o);

        Order DeleteFromExistingOrder(Offer o);

        bool PayForOrder(Order o);
        
    }
}
