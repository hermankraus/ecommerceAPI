namespace ecommerceAPI.Models
{
    public class CustomerDTO : UserDTO
    {
        public ICollection<OrderDTO> Cart { get; set; } = new List<OrderDTO>();
    }
}
