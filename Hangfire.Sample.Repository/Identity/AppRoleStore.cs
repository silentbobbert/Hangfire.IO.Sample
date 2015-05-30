using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Hangfire.Sample.Repository.Identity
{
    public class AppRoleStore : RoleStore<AppRole, Guid, AppUserRole>
    {
        public AppRoleStore(DbContext context) : base(context)
        {
        }
    }
}
