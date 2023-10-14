using ecommerceAPI.Entities;

namespace ecommerceAPI.Entities
{
    public class Orders
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public bool Status { get; set; }


        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
