using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Hangfire.Sample.Repository.Identity
{
    public class AppUserLogin : IdentityUserLogin<Guid> { }
}
