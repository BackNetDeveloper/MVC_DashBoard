using System.ComponentModel.DataAnnotations;

namespace Project.PresentationLayer_PL_.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Email Is Invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
