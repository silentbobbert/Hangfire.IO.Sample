using Hangfire.Sample.Repository.EF;
using Hangfire.Sample.Repository.Identity;
using System;
using System.Data.Entity.Migrations;

namespace Hangfire.Sample.Repository.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Roles.AddOrUpdate(r => r.Id, 
                new AppRole { Id = Guid.Parse("A843A55F-6B90-4F66-8666-B5155A5735D6"), Name = "Admin"},
                new AppRole { Id = Guid.Parse("C7E132F8-CEFA-4EDE-9F0D-0C8D791EBDFB"), Name = "SuperUser" },
                new AppRole { Id = Guid.Parse("B142996D-EAC5-431E-9331-3436E5AA92C9"), Name = "Basic" }
            );
        }
    }
}
