namespace ecommerceAPI.Models
{
    public class NewUserFromAdminDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? UserRole { get; set; }
        public bool State { get; set; }

    
    }
}
