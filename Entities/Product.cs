using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceAPI.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

            public string? Name { get; set; }

            public string? Description { get; set; }

            public double Price { get; set; } = 0;

            public List<OrderProduct>? OrderProducts { get; set; }

        }
    }

