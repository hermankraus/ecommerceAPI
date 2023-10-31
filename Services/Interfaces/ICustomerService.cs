using ecommerceAPI.Entities;
public interface ICustomerService
{
    Order CreateOrder(int userId);
    void AddProductsToOrder(int orderId, List<Product> products, List<int> quantities);
    List<Order> GetOrderHistory(int userId);
}
