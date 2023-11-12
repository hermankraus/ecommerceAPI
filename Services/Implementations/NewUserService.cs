using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;
using ecommerceAPI.Models;
using ecommerceAPI.Services.Interfaces;

namespace ecommerceAPI.Services.Implementations
{
    public class NewUserService : INewUserService
    {
        private readonly EcommerceContext _context;

        public NewUserService(EcommerceContext context)
        {
            _context = context;


        }

        public void CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
