using Company.G03.DAL.Model;
using Company.G03.PL.Helper;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signIn;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signIn)
        {
            this.userManager = userManager;
            this.signIn = signIn;
        }
        //Register
        //Login
        //Sign Out
        //Forgrt Pass
        //Ress Pass
        #region Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //P@ssw0rd
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,
                };
                var res = await userManager.CreateAsync(User, model.Password);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                }
            }
            return View(model);
        }
        #endregion
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var flag = await userManager.CheckPasswordAsync(User, model.Password);
                    if (flag)
                    {
                        var res = await signIn.PasswordSignInAsync(User, model.Password, model.RemeberMe, false);
                        if (res.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect Password");
                    }
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email is not exist");
            }
            return View();
        } 
        #endregion
        public new async Task<IActionResult> SignOut()
        {
           await signIn.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public async Task< IActionResult> SendMail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null) 
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetLink = Url.Action("ResetPassword","Account",new {email = model.Email,token=Token},Request.Scheme);

                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body =ResetLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Not Found");
                }

            }
            
                return View(model);
            

        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
    }
}
