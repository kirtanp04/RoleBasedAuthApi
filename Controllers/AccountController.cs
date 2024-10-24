using Authorization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register obj)
        {
            try
            {
                var user = new IdentityUser { UserName = obj.Username };
                var isSuccess = await _userManager.CreateAsync(user,obj.Password);

                if (isSuccess.Succeeded)
                {
                    return Ok(new { Success = true,message="Created new user" });
                }
                return BadRequest(new { Success = false, message = "Error occured while creating new user" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
               
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login obj)
        {
            try
            {
               var user = await _userManager.FindByNameAsync(obj.Username);

                if(user != null && await _userManager.CheckPasswordAsync(user,obj.Password))
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,user.UserName!),
                        new Claim(JwtRegisteredClaimNames.Sub,Guid.NewGuid().ToString())
                    };

                    authClaims.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role)));

                    var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                        expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"]!)),
                        claims: authClaims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                        SecurityAlgorithms.HmacSha256
                        ));
                         return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
                };
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            try
            {
                if(!await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role));

                    if (result.Succeeded) 
                    {
                        return Ok(new { message="Successfully created new role" });
                    }
                    return BadRequest(result.Errors);
                }

                return BadRequest("Role already exist");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] Role obJRole)
        {
            try
            {
              var user = await _userManager.FindByNameAsync(obJRole.Username);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var result = await _userManager.AddToRoleAsync(user, obJRole.RoleName);
                if (result.Succeeded)
                {
                    return Ok(new { message =" Successfully assign new role to" + user.UserName});
                }

                return BadRequest(result.Errors);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
