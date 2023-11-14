using Microsoft.AspNetCore.Mvc;
using ecommerceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using ecommerceAPI.Models;
using ecommerceAPI.Services.Interfaces;
using System.Security.Claims;

namespace ecommerceAPI.Controllers
{
    [Route("api/admin/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly INewUserService _newUserService;
        private readonly IUserService _userService;
        
        public AdminController(IAdminService adminService, INewUserService newUserService, IUserService userService)
        {
            _adminService = adminService;
            _newUserService = newUserService;
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
        public IActionResult AddProduct(ProductDTO product)
        {
            try
            {
                _adminService.AddProduct(product);
                //return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Product is null");
            }
        }

        [HttpPut("EditProduct{id}")]
        public IActionResult EditProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                _adminService.EditProduct(product);
                return NoContent();
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Product is null");
            }
        }

        [HttpDelete("DeleteProduct{id}")]
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

        [HttpPut("AdminUpdateCustomer")]

        public IActionResult UpdateCustomer([FromBody] UserDTO updateCustomer, int userId)
        {
           
            var userToUpdate = _userService.GetUser(userId);
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

        [HttpDelete("AdminDeleteCustomer")]

        public IActionResult DeleteCustomer()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            _userService.DeleteUser(id);

            return Ok();

        }

        [HttpPost("AdminCreateNewUser")]

        public ActionResult<User> CreateUser([FromBody] UserDTO userDTO)
        {
            var user = new Customer
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Address = userDTO.Address,
                UserRole = "Customer",

            };
            _newUserService.CreateUser(user);
            return Ok(user);
        }
    }
}
