using Example.JwtToken.Dtos;
using Example.JwtToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Example.JwtToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class LoginController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public LoginController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GirisYap([FromBody] UserDto userDto)
        {
            var user = UserList.AllUsers.Where(x => x.UserName == userDto.UserName && x.Password == userDto.Password).FirstOrDefault();
            if (user == null)
            {
                return Ok("Giriş yapılamadı!");
            }
            var token = SetToken(user);
            return Ok(token);
        }
        private string SetToken(User user)
        {
            string tokenStr = "";
            try
            {
                if (_jwtSettings.Key == null)
                {
                    throw new Exception("Key null olamaz!");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                };
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials);
                tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

            }
            return tokenStr;
        }
    }
}
