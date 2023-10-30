using ecommerceAPI.Entities;
using ecommerceAPI.Models;
using ecommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ecommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;

        public AuthController(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost]
        public IActionResult Auth([FromBody]CredentialsDTO credentialsDTO)
        {
            //Paso 1
            Tuple<bool,User?> validationResponse = _userService.ValidateUser(credentialsDTO.Email, credentialsDTO.Password);

            if(!validationResponse.Item1 && validationResponse.Item2 == null) 
            {
                return NotFound();
            }
            else if(!validationResponse.Item1 && validationResponse.Item2 != null)
                return Unauthorized();
            
            //Paso 2
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", validationResponse.Item2.Id.ToString())); 
            claimsForToken.Add(new Claim("given_name", validationResponse.Item2.Name)); 
            
            //Paso 3

            var jwtSecurityToken = new JwtSecurityToken(
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);

        }
    
    }
}
