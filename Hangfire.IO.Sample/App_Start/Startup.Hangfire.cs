using Hangfire.Dashboard;
using Hangfire.Unity;
using Microsoft.Practices.Unity;
using Owin;

namespace Hangfire.IO.Sample
{
    public partial class Startup
    {
        public void ConfigureHangfire(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            GlobalConfiguration.Configuration.UseActivator(new UnityJobActivator(UnityConfig.GetConfiguredContainer() as UnityContainer));

            var options = new DashboardOptions
            {
                AuthorizationFilters = new IAuthorizationFilter[] 
                {
                    //new AuthorizationFilter { Roles = "Admin" },
                    new ClaimsBasedAuthorizationFilter("hangfireAccess", "true")
                }
            };
            app.UseHangfireDashboard("/hangfire", options);
            
            //app.UseHangfireServer(); //Enable this line if you want the server process to also process queued jobs.
        }
    }
}