using System.ComponentModel.DataAnnotations;

namespace Project.PresentationLayer_PL_.Models.Account
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Email Is Invalid")]
        public string Email { get; set; }
    }
}
