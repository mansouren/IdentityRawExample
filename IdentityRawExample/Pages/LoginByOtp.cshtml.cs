using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityRawExample.Data;
using IdentityRawExample.Senders;
using IdentityRawExample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityRawExample.Pages
{
    public class LoginByOtpModel : PageModel
    {
        private readonly CustomUserManager customUserManager;
        private readonly ISmsSender smsSender;

        [BindProperty]
        public LoginByOtpViewModel loginByOtp { get; set; }

        public LoginByOtpModel(CustomUserManager customUserManager,ISmsSender smsSender)
        {
            this.customUserManager = customUserManager;
            this.smsSender = smsSender;
        }
        public string ReturnUrl { get; set; }
        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await customUserManager.GetUserByPhoneNumber(loginByOtp.PhoneNumber);
                if (user != null)
                {
                    
                    Random rnd = new Random();
                    string otpcode = rnd.Next(100000, 900000).ToString();
                    user.OTPCode = otpcode;
                    user.OtpExpiry = DateTime.Now.AddMinutes(5);
                    await customUserManager.UpdateAsync(user);
                    await smsSender.SendSms(loginByOtp.PhoneNumber, $"کد یکبار مصرف شما :{otpcode} میباشد");
                    ReturnUrl = returnUrl;
                    return RedirectToPage("verifyOtpCode", returnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("", "کاربری با این مشخصات یافت نشد");
                return Page();
            }
            return Page();
        }
    }
}
