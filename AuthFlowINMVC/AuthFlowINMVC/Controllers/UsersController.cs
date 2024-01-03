using AuthFlowINMVC.Models;
using AuthFlowINMVC.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthFlowINMVC.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string? gidilecekSayfa)
        {
            ViewBag.ReturnUrl = gidilecekSayfa;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? gidilecekSayfa)
        {
            //Clear: 123456
            //Hashed: A00!BFF'E'^fDF;
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword("123456");
            //ViewBag.Message = BCrypt.Net.BCrypt.Verify("123456", passwordHash) ? "Eşleşti" : "Farklı";
            //ViewBag.PasswordHash = passwordHash;
            //123456

            if (ModelState.IsValid)
            {
                var user = new UserService().ValidateUser(loginViewModel.UserName, loginViewModel.Password);
                if (user != null)
                {
                    var claims = new Claim[]{
                        new Claim(ClaimTypes.Name, loginViewModel.UserName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (!string.IsNullOrEmpty(gidilecekSayfa) && Url.IsLocalUrl(gidilecekSayfa))
                    {
                        return Redirect(gidilecekSayfa);
                    }
                    return Redirect("/");
                }
                ModelState.AddModelError("login", "Hatalı giriş...");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
