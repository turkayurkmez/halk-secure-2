using JWTWithRest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTWithRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {

            //geriye token döndür....
            if (ModelState.IsValid)
            {
                var user = userService.ValidateUser(userLogin.UserName, userLogin.Password);
                if (user != null)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("onay-icin-gereken-cumle"));
                    var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),

                    };
                    var token = new JwtSecurityToken(
                        issuer: "halkbank.server",
                        audience: "halkbank.client",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credential
                      );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }

                ModelState.AddModelError("login", "Hatalı giriş");
            }

            return BadRequest(ModelState);

        }
    }
}
