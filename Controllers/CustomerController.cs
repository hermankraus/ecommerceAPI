using Microsoft.AspNetCore.Mvc;
using ecommerceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using ecommerceAPI.Services.Interfaces;
using System.Security.Claims;
using ecommerceAPI.Models;

namespace ecommerceAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomerController(ICustomerService customerService, IUserService userService)
        {
            _userService = userService;
            _customerService = customerService;
        }

        [HttpPost("createOrder/{userId}")]
        public ActionResult<Order> CreateOrder(int userId)
        {
            var order = _customerService.CreateOrder(userId);
            return CreatedAtAction(nameof(order), new { id = order.Id }, order);
        }

        [HttpPost("addProductsToOrder/{orderId}")]
        public IActionResult AddProductsToOrder(int orderId, [FromBody] OrderProductsRequest request)
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

        // Create customer tiene que ir en un usercontroller porq user solamente va a crear nuevos usuarios
        /*
        [HttpPost("createCustomer")]
        public IActionResult CreateCustomer([FromBody] CustomerDTO dto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString(); ;
            if (role == "Customer")
            {
                return Ok("Cliente creado");
            }
            return Forbid();
        }
        */


        [HttpPut("UpdateCustomer/{updateCustomer}")]

        public IActionResult UpdateCustomer([FromBody] CustomerDTO updateCustomer)
        {
        
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Customer")
            {
                _userService.UpdateUser(updateCustomer);
        
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete("DeleteCustomer/{userId}")]

        public IActionResult DeleteCustomer(int userId)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            _userService.DeleteUser(id);

            return Ok();

        }

    }
}

public class OrderProductsRequest
{
    public List<Product> Products { get; set; }
    public List<int> Quantities { get; set; }
}
