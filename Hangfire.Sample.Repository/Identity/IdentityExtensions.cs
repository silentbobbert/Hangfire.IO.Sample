using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Hangfire.Sample.Repository.Identity
{
    public static class IdentityExtensions
    {
        public static Guid GetUserIdAsGuid(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci == null) return default(Guid);
            var id = ci.FindFirstValue(ClaimTypes.NameIdentifier);
            return id != null ? new Guid(id) : default(Guid);
        }
    }
}
