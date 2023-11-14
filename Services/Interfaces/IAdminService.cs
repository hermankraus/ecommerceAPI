
using ecommerceAPI.Entities;
using ecommerceAPI.Models;

public interface IAdminService
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    void AddProduct(AddProductToTableDTO product);
    void EditProduct(Product product);
    void DeleteProduct(int id);
    void CreateNewUserFromAdmin(NewUserFromAdminDTO NewUserFromAdminDTO);
    List<Customer> GetAllCustomers();
}
