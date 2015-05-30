using Hangfire.Sample.Repository.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Hangfire.Sample.Repository.Identity
{
    public class AppRole : IdentityRole<Guid, AppUserRole> { }
}
