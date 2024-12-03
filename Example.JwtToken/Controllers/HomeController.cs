using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Example.JwtToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Yönetici,Standart")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string name = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(name);
        }
    }
}
