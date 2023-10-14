using ecommerceAPI.Entities;

namespace ecommerceAPI.Entities
{
    public class Customer : User
    {
        public string Address { get; set; }
        public object? Order { get; internal set; }
    }
}
