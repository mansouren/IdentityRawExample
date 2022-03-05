using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityRawExample.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace IdentityRawExample.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<LoginWith2faModel> _logger;
        private readonly IEmailSender emailSender;

        public LoginWith2faModel(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager ,
            ILogger<LoginWith2faModel> logger, 
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            this.userManager = userManager;
            _logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
        public string Email { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }

            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string email,bool rememberMe, string returnUrl = null)
        {
            var user1 = await userManager.FindByEmailAsync(email);
            if(user1 == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }
            var providers = await userManager.GetValidTwoFactorProvidersAsync(user1);
            if (!providers.Contains("Email"))
            {
                return Page();
            }
            var token = await userManager.GenerateTwoFactorTokenAsync(user1, "Email");
            //var message = new Message(new string[] { email }, "Authentication token", token, null);
            await emailSender.SendEmailAsync(email,"Confirmation Code",token);
            ReturnUrl = returnUrl;
            RememberMe = rememberMe;
            return Page();
          
        }

        public async Task<IActionResult> OnPostAsync( bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();//We get the user from Identity.TwoFactorUserId cookie
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            //var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);
            var result = await _signInManager.TwoFactorSignInAsync("Email", authenticatorCode, rememberMe, Input.RememberMachine);
            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
        }
    }
}
