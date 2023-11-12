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

   

        [HttpPut("UpdateCustomer")]

        public IActionResult UpdateCustomer([FromBody] UserDTO updateCustomer)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var userToUpdate = _userService.GetUser(int.Parse(userId));
            if (userToUpdate == null)
            {
                return BadRequest();
            }
            userToUpdate.Name = updateCustomer.Name;
            userToUpdate.Email = updateCustomer.Email;
            userToUpdate.Address = updateCustomer.Address;
            userToUpdate.Password = updateCustomer.Password;

            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Customer")
            {
                _userService.UpdateUser(userToUpdate);
        
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete("DeleteCustomer")]

        public IActionResult DeleteCustomer()
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
