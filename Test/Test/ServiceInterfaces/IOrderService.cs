using JobPortal.Model;


namespace ServiceLibrary.ServiceInterfaces
{
    public interface IOrderService
    {
        Order CreateOrder(User u, Offer o, int hours);

        bool CancelOrder(Order o);

        Order AddToExistingOrder(Offer o);

        Order DeleteFromExistingOrder(Offer o);

        bool PayForOrder(Order o);
        
    }
}
