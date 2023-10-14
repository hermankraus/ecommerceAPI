using System.ComponentModel.DataAnnotations;

namespace ecommerceAPI.Entities
{
    public class Product
    {
        [Key]    
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public decimal Price { get; set; } = 0;

            public int Stock { get; set; }

        }
    }

