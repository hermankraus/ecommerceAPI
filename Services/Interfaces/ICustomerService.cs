using ecommerceAPI.Entities;
using ecommerceAPI.Models;

public interface ICustomerService
{
    void CreateOrder(int userId, List<ProductOrderDTO> products);
    void CancelOrder(Order canceledOrder);
    List<ShowProductsOrderDTO> GetOrderHistory(int userId);
}
