using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HealthFitnessServer.DBModel;
using HealthFitnessServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthFitnessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Login([FromBody] LoginModel login)
        {
            HealthFitnessContext context = new HealthFitnessContext();
            User user = context.Users.FirstOrDefault(x => x.Email.ToLower() == login.Email.ToLower()&&x.PasswordHash==login.PasswordHash);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Email",login.Email),
                        new Claim("Email",user.Email),
                        new Claim("PasswordHash",login.PasswordHash),
                        new Claim("PasswordHash",user.PasswordHash),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                       _configuration["Jwt:Issuer"],
                       _configuration["Jwt:Audience"],
                       claims,
                       expires: DateTime.UtcNow.AddMinutes(60),
                       signingCredentials: signIn
                    );
                string jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwttoken);
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }
    }
}

