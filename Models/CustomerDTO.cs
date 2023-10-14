namespace ecommerceAPI.Models
{
    public class CustomerDTO : UserDTO
    {
        public string Address { get; set; }

        public ICollection<OrderDTO> Cart { get; set; } = new List<OrderDTO>();
    }
}
