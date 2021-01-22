using CloudDataManagement.Extension;
using CloudDataManagement.Imp;
using CloudDataManagement.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CloudDataManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Startup.Run();

            var provider = services.BuildServiceProvider();
            Console.WriteLine("Start to Remove follow intune redundancy data...");
            Console.WriteLine("  (1)Device scripts.");
            Console.WriteLine("  (2)Security baseline & Endpoint detection and response.");
            Console.WriteLine("  (3)App configuration profile.");
            Console.WriteLine("  (4)Mobile apps.");
            Console.WriteLine("  (5)Compliance policy.");
            Console.WriteLine("  (6)Autopilot Deployment Profiles.");
            var intuneService = provider.GetService<IDeleteService>();
            intuneService.Delete().GetAwaiter().GetResult();
            Console.WriteLine("Please press any key to exist!");
            Console.ReadKey();
        }
    }
}
