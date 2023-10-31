using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;

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

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
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
    }
}
