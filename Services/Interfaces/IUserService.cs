using ecommerceAPI.Entities;
using ecommerceAPI.Models;

namespace ecommerceAPI.Services.Interfaces
{
    public interface IUserService
    { 
        public void UpdateUser (User user);
        public void DeleteUser (int userId);
        public User GetUser(int userId);
    }
}
