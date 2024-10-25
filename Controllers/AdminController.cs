using Authorization.Crypto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    //[Authorize(Policy = "AdminPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        [HttpGet("restricted")]
        public IActionResult Restricted()
        {
            // Optionally, use Forbid explicitly in this action
            if (!User.IsInRole("Admin"))
            {
                return Forbid("Access denied: Admin privileges required.");
            }

             var encryptedText = Crypto.Crypto.Encrypt("My name is kirtan");

            return Ok(encryptedText);
        }

        [HttpGet("decrypt/{text}")]
        public IActionResult RestrictedAsync(string text) 
        {
            var decryptedText = Crypto.Crypto.Decrypt(text);
            return Ok(decryptedText);
        }
    }
}
