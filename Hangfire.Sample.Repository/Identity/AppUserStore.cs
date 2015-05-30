using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Hangfire.Sample.Repository.Identity
{
    public class AppUserStore : UserStore<ApplicationUser, AppRole, Guid, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(DbContext context)
            : base(context)
        {

        }
    }
}