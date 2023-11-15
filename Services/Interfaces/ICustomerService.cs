using ecommerceAPI.Entities;
using ecommerceAPI.Models;

public interface ICustomerService
{
    void CreateOrder(int userId, List<ProductOrderDTO> products);
    void AddProductsToOrder(int orderId, List<Product> products, List<int> quantities);
    List<Order> GetOrderHistory(int userId);
}
