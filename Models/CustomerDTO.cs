namespace ecommerceAPI.Models
{
    public class CustomerDTO : UserDTO
    {
        public string Address { get; set; }

        public IList<ProductDTO> Cart { get; set; } = new List<ProductDTO>();
    }
}
