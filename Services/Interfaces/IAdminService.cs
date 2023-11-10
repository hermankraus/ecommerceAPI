
using ecommerceAPI.Entities;
using ecommerceAPI.Models;

public interface IAdminService
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    void AddProduct(ProductDTO product);
    void EditProduct(Product product);
    void DeleteProduct(int id);
    List<Customer> GetAllCustomers();
}
