using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.DataAccessLayer_DAL_.Entities;
using Project.PresentationLayer_PL_.Helper;
using Project.PresentationLayer_PL_.Models.Account;
using System.Threading.Tasks;

namespace Project.PresentationLayer_PL_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email.Split('@')[0],
                    IsAgree = registerViewModel.IsAgree,
                };
                var result = await userManager.CreateAsync(user,registerViewModel.Password);
                // PasswordRequiresNonAlphanumeric,PasswordRequiresDigit,PasswordRequiresUpper
                if (result.Succeeded)
                    return RedirectToAction("SignIn");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty,error.Description);
            }
            return View(registerViewModel);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var password = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (password)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememmberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                        
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email");
                }
            }
            return View(loginViewModel);

        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var resetpasswordlink = Url.Action("ResetPassword", "Account", new { Email = forgetPasswordViewModel.Email, Token = token },Request.Scheme);
                    var email = new Email() 
                    {
                        Tittle = "Reset Password",
                        Body = resetpasswordlink,
                        To = forgetPasswordViewModel.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CompleteForgetPassword");
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(forgetPasswordViewModel);
        }
        public IActionResult CompleteForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string email ,string token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user is not null)
                {
                    var result = await userManager.ResetPasswordAsync(user,resetPasswordViewModel.Token, resetPasswordViewModel.Password);
                    if (result.Succeeded)
                        return RedirectToAction("ResetPasswordDone");

                    foreach (var Error in result.Errors)
                        ModelState.AddModelError(string.Empty, Error.Description);
                }
            }
            return View(resetPasswordViewModel);
        }
        public IActionResult ResetPasswordDone()
        {
            return View();
        }
    }
}
