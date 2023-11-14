namespace ecommerceAPI.Models
{
    public class AddProductToTableDTO
    {

        public string? Name { get; set; }

        public string? Description { get; set; }
        public double Price { get; set; } = 0;


    }
}
