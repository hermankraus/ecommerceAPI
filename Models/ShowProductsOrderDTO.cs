using ecommerceAPI.Enums;

namespace ecommerceAPI.Models
{
    public class ShowProductsOrderDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime();

        public OrderStatus StatusOrder { get; set; }


        public ICollection<ProductOrderDTO> Products { get; set; } = new List<ProductOrderDTO>();
    }
}
