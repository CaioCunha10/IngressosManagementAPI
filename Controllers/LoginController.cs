using IngressosAPI.DTOs;
using IngressosAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IngressosAPI.Controllers
{
    [Route("api/[controller]")]
    //[ServiceFilter(typeof(AuthorizationFilter))]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Entrar")] 
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Os dados do login são inválidos.");
            }

             if (loginDTO.Username == "user1" && loginDTO.Password == "password1")
            {
                var token = GenerateJwtToken(loginDTO.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciais inválidas.");
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
