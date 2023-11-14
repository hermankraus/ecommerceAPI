using ecommerceAPI.Enums;

namespace ecommerceAPI.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public OrderStatus StatusOrder { get; set; }


        public ICollection<AddProductToTableDTO> Products { get; set; } = new List<AddProductToTableDTO>(); 
    }
}
