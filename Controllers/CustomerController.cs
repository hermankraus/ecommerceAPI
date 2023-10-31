using Microsoft.AspNetCore.Mvc;
using ecommerceAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ecommerceAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("createOrder/{userId}")]
        public ActionResult<Order> CreateOrder(int userId)
        {
            var order = _customerService.CreateOrder(userId);
            return CreatedAtAction(nameof(order), new { id = order.Id }, order);
        }

        [HttpPost("addProductsToOrder/{orderId}")]
        public ActionResult AddProductsToOrder(int orderId, [FromBody] OrderProductsRequest request)
        {
            if (request == null || request.Products == null || request.Quantities == null
                || request.Products.Count != request.Quantities.Count)
            {
                return BadRequest("La solicitud es inválida.");
            }

            try
            {
                _customerService.AddProductsToOrder(orderId, request.Products, request.Quantities);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("orderHistory/{userId}")]
        public ActionResult<IEnumerable<Order>> GetOrderHistory(int userId)
        {
            var orders = _customerService.GetOrderHistory(userId);
            return Ok(orders);
        }
    }
}

public class OrderProductsRequest
{
    public List<Product> Products { get; set; }
    public List<int> Quantities { get; set; }
}
