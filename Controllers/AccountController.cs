using Esercitazione.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Esercitazione.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthSvc authSvc;
        public AccountController(IAuthSvc authSvc)
        {
            this.authSvc = authSvc;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser user)
        {
            try
            {
                var u = authSvc.Login(user.Username, user.Password);
                if (u == null)
                    return RedirectToAction("Index");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.Username),

                };
                if (u.FriendlyName != null)
                    claims.Add(new Claim("FriendlyName", u.FriendlyName));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}