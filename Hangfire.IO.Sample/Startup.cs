using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(Hangfire.IO.Sample.Startup))]
namespace Hangfire.IO.Sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureHangfire(app);

            GlobalHost.DependencyResolver.UseRedis("localhost", 6379, null, "HangfireSample");
            app.MapSignalR();

            Hangfire.Sample.Repository.Configuration.Log = DependencyResolver.Current.GetService<ILog>();

        }
    }
}
