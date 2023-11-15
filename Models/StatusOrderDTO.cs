using ecommerceAPI.Enums;

namespace ecommerceAPI.Models
{
    public class StatusOrderDTO
    {
        public int orderId { get; set; }
        public OrderStatus StatusOrder { get; set; }

    }
}
