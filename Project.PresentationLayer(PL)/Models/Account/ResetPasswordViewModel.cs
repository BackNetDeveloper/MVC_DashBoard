using System.ComponentModel.DataAnnotations;

namespace Project.PresentationLayer_PL_.Models.Account
{
    public class ResetPasswordViewModel
    {
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
