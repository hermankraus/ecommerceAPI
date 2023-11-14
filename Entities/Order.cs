using ecommerceAPI.Entities;
using ecommerceAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceAPI.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus StatusOrder { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime();
        public List<OrderProduct>? OrderProducts { get; set; }


    }
}
