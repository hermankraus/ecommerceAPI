using ecommerceAPI.Entities;

namespace ecommerceAPI.Entities
{
    public class Customer : User
    {
        public string Address { get; set; }

        public IList<Product> Cart { get; set; } = new List<Product>();
    }
}
