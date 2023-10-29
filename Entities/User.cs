using ecommerceAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace ecommerceAPI.Entities
{
    public abstract class User
    {
        [Key] 
        public int Id { get; set; }  
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole? Role { get; set; }
        public List<Order>? Order { get; set; }

    }
}
