using ecommerceAPI.Entities;
using ecommerceAPI.Models;
using ecommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceAPI.Controllers
{
    [Route ("api/newUser")]
    [ApiController]
    public class NewUserServiceController : ControllerBase
    {
        private readonly INewUserService _newUserService;

        public NewUserServiceController (INewUserService newUserService)
        {
            _newUserService = newUserService;
        }

        [HttpPost("createnewuser")]

        public ActionResult<User> CreateUser([FromBody]UserDTO userDTO)
        {
            var user = new Customer
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Address = userDTO.Address.ToString(),
                UserRole = "Customer",

            };
            _newUserService.CreateUser(user);
            return Ok(user);
        }


    }
}
