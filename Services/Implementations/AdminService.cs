using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;
using ecommerceAPI.Models;

namespace ecommerceAPI.Services.Implementations
{

    public class AdminService : IAdminService
    {
        private readonly EcommerceContext _context;

        public AdminService(EcommerceContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO));
            }

            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
            };

            _context.Add(product);
            _context.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var productToDelete = _context.Products.FirstOrDefault(p => p.Id == id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
            }
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList(); 
        }
    }
}
