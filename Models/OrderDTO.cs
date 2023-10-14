namespace ecommerceAPI.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public bool Status { get; set; }


        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>(); 
    }
}
