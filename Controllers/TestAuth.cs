using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuth : ControllerBase
    {
        [HttpGet("test-auth")]
        public IActionResult Get()
        {
            var isAuthenticated = HttpContext.User.Identity?.IsAuthenticated;
            var userName = HttpContext.User.Identity?.Name;
            var userClaims = HttpContext.User.Claims;

            return Ok(new
            {
                IsAuthenticated = isAuthenticated,
                UserName = userName,
                Claims = userClaims.Select(c => new { c.Type, c.Value })
            });
        }

    }
}
