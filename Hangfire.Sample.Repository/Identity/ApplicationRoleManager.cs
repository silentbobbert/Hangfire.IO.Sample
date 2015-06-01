using Microsoft.AspNet.Identity;
using System;

namespace Hangfire.Sample.Repository.Identity
{
    public class ApplicationRoleManager : RoleManager<AppRole, Guid>
    {
        public ApplicationRoleManager(IRoleStore<AppRole, Guid> roleStore)
            : base(roleStore)
        {
        }
    }
}
