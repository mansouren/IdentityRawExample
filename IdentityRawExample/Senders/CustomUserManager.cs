using IdentityRawExample.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityRawExample.Senders
{
    public class CustomUserManager : Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>
    {
        public CustomUserManager(Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<ApplicationUser> GetUserByPhoneNumber(string phone)
        {
            return await Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
        }
    }
}
