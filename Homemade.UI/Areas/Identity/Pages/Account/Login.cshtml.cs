using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Homemade.Model;
using Microsoft.IdentityModel.Tokens;

namespace Homemade.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<User> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = new User { UserName = Input.Email, Email = Input.Email };
            var resultdddddd = await _userManager.CreateAsync(user, Input.Password);

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }

                else if (result.IsNotAllowed)
                {
                    // more specific/user-friendly error :)
                    ModelState.AddModelError("EmailNotConfirmed", "The email address is not confirmed");
                    return Page();
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("LockedOut", "This account is locked out");
                    return Page();
                }
                else
                {
                    ModelState.AddModelError("IncorrectCredentials", "Email or Password is incorrect.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        //public async Task<IActionResult> SignInAsync([Bind("Email,Password,RememberMe")] SigningCredentials credentials)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _signInManager
        //        .PasswordSignInAsync(credentials.Email, credentials.Password, credentials.RememberMe)
        //        .ConfigureAwait(false);

        //    if (result.Succeeded)
        //    {
        //        var user = await _userManager.FindByEmailAsync(credentials.Email)
        //            .ConfigureAwait(false);

        //        return Ok(user);
        //    }
        //    else if (result.EmailNotConfirmed)
        //    {
        //        // more specific/user-friendly error :)
        //        ModelState.AddModelError("EmailNotConfirmed", "The email address is not confirmed");
        //        return BadRequest(ModelState);
        //    }
        //    else if (result.IsLockedOut)
        //    {
        //        ModelState.AddModelError("LockedOut", "This account is locked out");
        //        return BadRequest(ModelState);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("IncorrectCredentials", "Email or Password is incorrect.");
        //        return BadRequest(ModelState);
        //    }
        //}
    }
}
