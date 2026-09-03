using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Wherrelz_Crud.Data;
using Wherrelz_Crud.ViewModels;

namespace Wherrelz_Crud.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.LoginId == model.LoginId && x.Password == model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Login ID or Password");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim("UserId", user.LoginId),
                     new Claim("AppInstanceId", AppSession.InstanceId.ToString())
                };

                var identity = new ClaimsIdentity(claims,"MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth",principal);

                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Index", "Login");

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index", "Login");
        }
    }

}
