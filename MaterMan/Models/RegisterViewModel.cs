using System.ComponentModel.DataAnnotations;

namespace MaterMan.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı ismi gereklidir.")]
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Ad gereklidir.")]
        [Display(Name = "Adınız")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir.")]
        [Display(Name = "Soyadınız")]
        public string? Surname { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9\s,.'-]{5,200}$", ErrorMessage = "Adres geçersiz formatta.")]
        [Required(ErrorMessage = "Adres gereklidir.")]
        [Display(Name = "Adres")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Departman gereklidir.")]
        [Display(Name = "Departmanınız")]
        public string? Department { get; set; }

        [Required(ErrorMessage = "TC Kimlik No gereklidir.")]
        [Display(Name = "TC Kimlik No")]
        [StringLength(11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır.", MinimumLength = 11)]
        public string? IdentityCard { get; set; }

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [Display(Name = "Email Adresiniz")]
        [EmailAddress(ErrorMessage = "Email adresiniz doğru formatta değil.")]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
