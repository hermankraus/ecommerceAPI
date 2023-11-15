using ecommerceAPI.Entities;
using ecommerceAPI.Models;

public interface ICustomerService
{
    void CreateOrder(int userId, List<ProductOrderDTO> products);
    Order GetOrderByOrderId(int orderId);
    void CancelOrder(Order canceledOrder);
    List<Order> GetOrderHistory(int userId);
}
