using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        private Users AuthenticateUser(Users user)
        {
            Users _user = null;
           
            if (user.Username == "admin" && user.Password == "12345")
            {
                _user = new Users { Username="Vaishali Ghodke"};

            }
            return _user;
        }
        private string GenerateToken(Users  users)
        {
            var Securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var Credentials =new SigningCredentials(Securitykey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt: Audience"], null,
                expires:DateTime.Now.AddMinutes(1),
        signingCredentials: Credentials
                );
            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
