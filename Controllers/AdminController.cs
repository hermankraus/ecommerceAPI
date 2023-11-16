using Microsoft.AspNetCore.Mvc;
using ecommerceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using ecommerceAPI.Models;
using ecommerceAPI.Services.Interfaces;
using ecommerceAPI.Enums;
using System.Security.Claims;

namespace ecommerceAPI.Controllers
{
    [Route("api/admin/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
            
        }

        [HttpGet ("GetProducts")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _adminService.GetProducts();
            return Ok(products);
        }

        [HttpGet("GetProductById{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _adminService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(AddProductToTableDTO product)
        {
            try
            {
                _adminService.AddProduct(product);
                
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Product is null");
            }
        }

        [HttpPut("EditProduct{id}")]
        public IActionResult EditProduct(int id, AddProductToTableDTO productdto)
        {
            var productSelected = _adminService.GetProductById(id);
            if (id != productSelected.Id)
            {
                return BadRequest();
            }

            try
            {
                productSelected.Name = productdto.Name;
                productSelected.Description = productdto.Description;
                productSelected.Price = productdto.Price;

                _adminService.EditProduct(productSelected);
                return Ok("Producto Modificado");
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Product is null");
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _adminService.DeleteProduct(id);
                return NoContent();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }


        [HttpGet ("GetAllCustomers")]
        public IActionResult GetAllCustomers() {
            var customers = _adminService.GetAllCustomers();
            
            return Ok(customers);
        }
        [HttpPost("CreateNewUserByAdmin")]
        public IActionResult CreateNewUserByAdmin([FromBody] NewUserFromAdminDTO newUserFromAdminDTO)
        {
            if (newUserFromAdminDTO.UserRole == "Admin" || newUserFromAdminDTO.UserRole == "Customer")
            {
                _adminService.CreateNewUserFromAdmin(newUserFromAdminDTO);

                return Ok($"New {newUserFromAdminDTO.UserRole} {newUserFromAdminDTO.Name} Created");
            }
            return BadRequest("UserRole Incorrect");
        }

        [HttpGet("GetUserById")]

        public IActionResult GetUserById() 
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var userToShow = _userService.GetUserById(id);

            UserDTO userDTO = new UserDTO()
            {
                Name = userToShow.Name,
                Email = userToShow.Email,
                Password = userToShow.Password,
                Address = userToShow.Address,
            };
            

            return Ok(userDTO);

        }


        [HttpPut("UpdateUser/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] NewUserFromAdminDTO user)
        {   
            var userToUpdate = _userService.GetUser(userId);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.Address = user.Address;
            userToUpdate.State = user.State;

            _userService.UpdateUser(userToUpdate);
            return Ok(userToUpdate);
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpPut("modifyOrder")]

        public IActionResult ModifyStatusOrder([FromBody] StatusOrderDTO statusOrderDTO)
        {
            var orderToModify = _userService.GetOrderByOrderId(statusOrderDTO.orderId);

            if (orderToModify == null)
            {
                return NotFound("Orden no existente");
            }

            orderToModify.StatusOrder = statusOrderDTO.StatusOrder;

            if (orderToModify.StatusOrder == OrderStatus.Approved || orderToModify.StatusOrder == OrderStatus.Waiting || orderToModify.StatusOrder == OrderStatus.Canceled)
            {
                _adminService.ModifyStatusOrder(orderToModify);
                return Ok($"Orden {statusOrderDTO.orderId} {statusOrderDTO.StatusOrder}");
            }
            else
                return BadRequest("Estado no existente");


        }

    }
}
