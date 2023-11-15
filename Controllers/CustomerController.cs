using Microsoft.AspNetCore.Mvc;
using ecommerceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using ecommerceAPI.Services.Interfaces;
using System.Security.Claims;
using ecommerceAPI.Models;
using ecommerceAPI.Enums;

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

        [HttpPost("createOrder/")]
        public IActionResult CreateOrder([FromBody] OrderRequestDTO orderRequest)
        {
            if (orderRequest == null || orderRequest.Products == null || orderRequest.Products.Count == 0)
            {
                return BadRequest("Orden Invalida");
            }

            try
            {
                
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                _customerService.CreateOrder(userId, orderRequest.Products);

                return Ok("La orden se creo con exito");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error creando la orden: {ex.Message}");
            }
        }

        [HttpPut("cancelOrder")]

        public IActionResult CancelOrder([FromBody] int orderId)
        {
            var orderToCancel = _userService.GetOrderByOrderId(orderId);

            if (orderToCancel == null)
            {
                return NotFound("Orden no existente");
            }

            orderToCancel.StatusOrder = OrderStatus.Canceled;

            _customerService.CancelOrder(orderToCancel);


            return Ok($"Orden {orderId} Cancelada");
        }




        [HttpGet("orderHistory")]
        public List<ShowProductsOrderDTO> GetOrderHistory()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var history = _customerService.GetOrderHistory(userId);

            return history;
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
