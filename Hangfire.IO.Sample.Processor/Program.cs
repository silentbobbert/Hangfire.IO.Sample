using Hangfire.IO.Sample.BusinessLogic.IoC;
using Hangfire.Unity;
using Microsoft.AspNet.SignalR;
using System;
using Microsoft.Practices.Unity;

namespace Hangfire.IO.Sample.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            GlobalConfiguration.Configuration.UseActivator(new UnityJobActivator(UnityConfig.GetConfiguredContainer() as UnityContainer));

            GlobalHost.DependencyResolver.UseRedis("localhost", 6379, null, "HangfireSample");

            using (var server = new BackgroundJobServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1,
                ServerName = "Console App Hosted Server"
            }))
            {
                
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
