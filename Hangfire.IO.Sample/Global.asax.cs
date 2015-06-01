using System.Configuration;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hangfire.IO.Sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                //http://blogs.msdn.com/b/webdev/archive/2014/04/09/ef-code-first-migrations-deployment-to-an-azure-cloud-service.aspx
                var conf = new Hangfire.Sample.Repository.Migrations.Configuration();
                var migrator = new DbMigrator(conf);
                migrator.Update();
            }

        }
    }
}
