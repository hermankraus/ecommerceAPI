using System.ComponentModel.DataAnnotations;

namespace ecommerceAPI.Models
{
    public class CredentialsDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
