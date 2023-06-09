using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtToken _jwtToken;
        public AuthController(IJwtToken jwtToken)
        {
            _jwtToken = jwtToken;
        }
        [HttpGet]
        public async Task<Token> Get() { 
            return await _jwtToken.GetAuthToken();

        }

    }
}
