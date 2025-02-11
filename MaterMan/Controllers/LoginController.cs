using MaterMan.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaterMan.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;  // UserManager'ı ekleyelim

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;  // UserManager'ı constructor'a ekliyoruz
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Kullanıcıyı email ile bulalım
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ModelState.AddModelError("", "Geçersiz giriş bilgileri.");
                return View("Index");
            }

            // Şifreyi kontrol edelim
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);


            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");  // Giriş başarılı ise Home/Index'e yönlendir
            }

            ModelState.AddModelError("", "Geçersiz giriş bilgileri.");
            return View("Index");  // Eğer giriş başarısızsa tekrar login sayfasına dön
        }
    }
}