using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;
using ecommerceAPI.Models;
using ecommerceAPI.Services.Interfaces;

namespace ecommerceAPI.Services.Implementations
{

    public class AdminService : IAdminService, IUserService
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

        public void AddProduct(AddProductToTableDTO productDTO)
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

        public Order GetOrderByOrderId(int orderId)
        {

            var order = _context.Orders.FirstOrDefault(u => u.Id == orderId);

            if (order != null)
            {
                return order;
            }

            return null;

        }

        public void ModifyStatusOrder(Order modifiedOrder)
        {
            _context.Update(modifiedOrder);
            _context.SaveChanges();
        }


        public void CreateNewUserFromAdmin(NewUserFromAdminDTO newUserDTO)
        {
            if (newUserDTO.UserRole == "Admin")
            {
                var newAdmin = new Admin
                {
                    Name = newUserDTO.Name,
                    Email = newUserDTO.Email,
                    Password = newUserDTO.Password,
                    Address = newUserDTO.Address,
                    UserRole = "Admin",
                };
                _context.Add(newAdmin);
                _context.SaveChanges();
            }
            else if (newUserDTO.UserRole == "Customer")
            {
                var newCustomer = new Customer
                {
                    Name = newUserDTO.Name,
                    Email = newUserDTO.Email,
                    Password = newUserDTO.Password,
                    Address = newUserDTO.Address,
                    UserRole = "Customer",
                };
                _context.Add(newCustomer);
                _context.SaveChanges();
            }

        }

        public void UpdateUser(User userToUpdate)
        {

            _context.Update(userToUpdate);
            _context.SaveChanges();

        }

        public void DeleteUser(int userId)
        {
            User userToDelete = _context.Users.FirstOrDefault(u => u.Id == userId);
            userToDelete.State = false;
            _context.Update(userToDelete);
            _context.SaveChanges();

        }
        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList(); 
        }
    }
}
