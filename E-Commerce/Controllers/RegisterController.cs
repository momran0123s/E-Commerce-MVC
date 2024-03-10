using E_Commerce.Models;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace E_Commerce.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly CommerceContext db;

        public RegisterController(UserManager<ApplicationUser> _userManager, CommerceContext _db)
        {
            userManager = _userManager;
            this.db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = registerVM.Name,
                    Email = registerVM.Email,
                    Address = registerVM.Address,
                    PasswordHash = registerVM.Password,
                };
                IdentityResult result = await userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    //await userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Login", "Login");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerVM);
        }
    }
}
