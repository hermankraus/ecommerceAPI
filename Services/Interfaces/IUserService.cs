using ecommerceAPI.Entities;
using ecommerceAPI.Models;

namespace ecommerceAPI.Services.Interfaces
{
    public interface IUserService
    {
        public void CreateUser (UserDTO user);
        public void UpdateUser (UserDTO user);
        public void DeleteUser (int userId);

    }
}
