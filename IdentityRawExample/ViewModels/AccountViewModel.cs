using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRawExample.ViewModels
{
    public class LoginByOtpViewModel
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا مقداری وارد کنید")]
        public string PhoneNumber { get; set; }
    }
}
