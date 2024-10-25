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
            if (!User.IsInRole("Admin"))
            {
                return BadRequest(new { error = "Access denied", message = "Admin privileges required." });
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
