using MaterMan.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using MaterMan.Services.EmailServices;
using System.Text.Encodings.Web;
using System.Net.Mail;
using System.Net;

namespace MaterMan.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, EmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCode(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu e-posta adresine ait kullanıcı bulunamadı.");
                return View("Index");
            }

            // Kullanıcının şifresini doğrula
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Geçersiz giriş bilgileri.");
                return View("Index");
            }

            // 6 haneli rastgele kod oluştur
            var verificationCode = new Random().Next(100000, 999999).ToString();

            // Session'a kaydet
            HttpContext.Session.SetString("VerificationCode", verificationCode);
            HttpContext.Session.SetString("UserEmail", email);

            // Kullanıcıya onay kodunu gönder
            await _emailService.SendEmailAsync(email, "Giriş Onay Kodu", $"Onay kodunuz: <b>{verificationCode}</b>");

            return RedirectToAction("VerifyCode");
        }

        [HttpGet]
        public IActionResult VerifyCode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCode(string code)
        {
            var storedCode = HttpContext.Session.GetString("VerificationCode");
            var email = HttpContext.Session.GetString("UserEmail");

            if (storedCode == code)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                    return View();
                }

                // Kullanıcıyı giriş yapmış olarak işaretle
                await _signInManager.SignInAsync(user, isPersistent: false);

                HttpContext.Session.Remove("VerificationCode"); // Kullanıldıktan sonra kodu temizle
                return RedirectToAction("Index", "Home"); // Giriş başarılı, anasayfaya yönlendir
            }
            ViewBag.Error = "Geçersiz veya yanlış kod!";
            return View();
        }





        //Şifremi Unuttum İşlemleri

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        //Şifremi Unuttum
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ViewBag.Error = "Bu e-posta ile bir kullanıcı bulunamamıştır!";
                return View();
            }


            //Sifreyi sıfırlamak icin token üretiyorum
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //sifresini sıfırlamak icin baglanti gönderiyorum
            var resetLink = Url.Action("ResetPassword", "Login", new { token, email = user.Email }, Request.Scheme);

            //Epostayı gönder
            await SendEmailAsync(user.Email, "Şifre Sıfırlama Talebi", $"Şifrenizi sıfırlamak için <a href='{HtmlEncoder.Default.Encode(resetLink)}'>buraya tıklayın</a>.");
            ViewBag.Message = "Şifre sıfırlama linki e-posta adresinize gönderildi.";

            return View();
        }

        
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("tlgkrks654@gmail.com", "qzwkbfopltyigvkm");
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("tlgkrks654@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {

            if(token==null || email==null)
            {
                ViewBag.Error = "Gecersiz Sifre Sifirlama Talebi";
                return View();
            }

            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı.";
                return View();
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!resetPassResult.Succeeded)
            {
                ViewBag.Error = "Şifre sıfırlama başarısız!";
                return View();
            }

            ViewBag.Message = "Şifreniz başarıyla sıfırlandı. Yeni şifrenizle giriş yapabilirsiniz.";
            return RedirectToAction("Index", "Login");
        }

    }
}
