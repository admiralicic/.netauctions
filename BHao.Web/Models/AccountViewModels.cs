using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BHao.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email adresa je obavezna.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage="Niste unijeli validnu email adresu.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje lozinka je obavezno.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage="Email adresa je obavezna.")]
        [EmailAddress(ErrorMessage="Niste unijeli validnu email adresu.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage="Polje lozinka je obavezno.")]
        [StringLength(100, ErrorMessage = "Lozinka mora imati najmanje {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda lozinke")]
        [Compare("Password", ErrorMessage = "Lozinka i potrda lozinke moraju biti iste.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage="Polje Ime je obavezno.")]
        public string Ime { get; set; }

        [Required( ErrorMessage = "Polje Prezime je obavezno." )]
        public string Prezime { get; set; }

        [Required( ErrorMessage = "Polje Ulica je obavezno." )]
        public string Ulica { get; set; }

        [Required( ErrorMessage = "Polje Broj je obavezno." )]
        public string Broj { get; set; }

        [Required( ErrorMessage = "Polje Poštanski broj je obavezno." )]
        [Display(Name="Poštanski broj")]
        public string PostanskiBroj { get; set; }

        [Required(ErrorMessage="Polje Grad je obavezno.")]
        public int GradId { get; set; }

        
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
