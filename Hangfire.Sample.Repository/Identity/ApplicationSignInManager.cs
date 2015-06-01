using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangfire.Sample.Repository.Identity
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            var identity = await user.GenerateUserIdentityAsync(UserManager);
            //identity.AddClaims(user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)));
            return identity;
        }
    }
}
