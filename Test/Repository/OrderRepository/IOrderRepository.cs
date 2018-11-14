using JobPortal.Model;
using Repository.DbConnection;

namespace Repository.OrderRepository
{
    public interface IOrderRepository: IRepository<OrderTable>
    {
        OrderTable CreateOrder(Users u);
        OrderTable AddToExistingOrder(OrderTable o, ServiceOffer s, int quantity);
        OrderTable DeleteFromExistingOffer(OrderTable o, ServiceOffer s);
    }
}
