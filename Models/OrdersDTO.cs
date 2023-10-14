namespace ecommerceAPI.Models
{
    public class OrdersDTO
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public bool Status { get; set; }


        public IList<ProductDTO> Products { get; set; } = new List<ProductDTO>(); 
    }
}
