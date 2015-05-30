using Hangfire.Sample.Repository.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Hangfire.Sample.Repository.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, Guid, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            
            
        }
        public virtual IDbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var logTable = new LogSetUp();
            logTable.Setup(modelBuilder);
            
            

        }
    }
}