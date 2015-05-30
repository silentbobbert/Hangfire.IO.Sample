using System;
using Microsoft.AspNet.SignalR;

namespace Hangfire.IO.Sample.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            GlobalHost.DependencyResolver.UseRedis("localhost", 6379, null, "HangfireSample");

            using (var server = new BackgroundJobServer())
            {
                
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
