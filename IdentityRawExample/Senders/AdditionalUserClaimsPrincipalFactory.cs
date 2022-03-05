//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace IdentityRawExample
//{
//    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
//    {
//        public AdditionalUserClaimsPrincipalFactory
//            (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
//             IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
//        {
//        }
//        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
//        {
//            var principle = await base.CreateAsync(user);
//            var identity = (ClaimsIdentity)principle.Identity;

//            var cliams = new List<Claim>();
//            if (user.TwoFactorEnabled)
//            {
//                cliams.Add(new Claim("amr", "mfa"));
//            }
//            else
//            {
//                cliams.Add(new Claim("amr", "pwd"));
//            }
            
//            identity.AddClaims(cliams);
//            return principle;

//        }
        
//    }
//}