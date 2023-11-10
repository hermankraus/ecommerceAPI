using Microsoft.AspNetCore.Mvc;
using ecommerceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using ecommerceAPI.Models;

namespace ecommerceAPI.Controllers
{
    [Route("api/admin/")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
            
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

        [HttpDelete("Delete{id}")]
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
    }
}
