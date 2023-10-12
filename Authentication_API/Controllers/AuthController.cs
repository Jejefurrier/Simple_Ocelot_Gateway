using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("token")]
        public IActionResult Get(string userName, string password, string userType)
        {
            //NOTE: This endpoint only generates the token, in a real scenario you should implement all safety store / read functions | No good practices here, it's just for educational purposes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("RANDOMKEYGENERATEDBYMYSELFANDSHOULDNOTBEUSED"); //Well, you should not store this here, please store in a safer place
            var issuer = "localhost:5253"; // this auth api ip:port
            var audience = "Public";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim("UserType", userType)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
