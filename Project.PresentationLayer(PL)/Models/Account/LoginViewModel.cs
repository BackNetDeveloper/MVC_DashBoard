using System.ComponentModel.DataAnnotations;

namespace Project.PresentationLayer_PL_.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Email Is Invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        public bool RememmberMe { get; set; }
    }
}
