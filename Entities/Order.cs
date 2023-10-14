using ecommerceAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceAPI.Entities
{
    public class Order
    {
        private ICollection<Customer>? customer;

        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime();

        public string Status { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        
        
    }
}
