
using ecommerceAPI.Entities;

public interface IAdminService
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    void AddProduct(Product product);
    void EditProduct(Product product);
    void DeleteProduct(int id);
}
