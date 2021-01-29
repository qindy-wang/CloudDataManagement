using CloudDataManagement.Extension;
using CloudDataManagement.Imp;
using CloudDataManagement.Inteface;
using CloudDataManagement.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CloudDataManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            //var services = Startup.Run();

            //var provider = services.BuildServiceProvider();
            ////Console.WriteLine("Start to Remove follow intune redundancy data...");
            ////Console.WriteLine("  (1)Device scripts.");
            ////Console.WriteLine("  (2)Security baseline & Endpoint detection and response.");
            ////Console.WriteLine("  (3)App configuration profile.");
            ////Console.WriteLine("  (4)Mobile apps.");
            ////Console.WriteLine("  (5)Compliance policy.");
            ////Console.WriteLine("  (6)Autopilot Deployment Profiles.");
            ////var intuneService = provider.GetService<IIntuneService>();
            ////intuneService.Delete().GetAwaiter().GetResult();

            //UpdateTest(provider);
            //Console.WriteLine("Please press any key to exist!");
            //Console.ReadKey();

            ObjectMapperTest();
        }
        private static void UpdateTest(ServiceProvider provider)
        { 
            var aadService = provider.GetService<IAADService>();
            aadService.Update().GetAwaiter().GetResult();
        }

        private static void AddTest(ServiceProvider provider)
        {
            var aadService = provider.GetService<IAADService>();
            aadService.Add().GetAwaiter().GetResult();
        }

        private static void ObjectMapperTest()
        {
            var graphClient = GraphClient.GetInstance("d6e01331-be4e-4114-86f1-09f2a9252679", "46514c3a-1b90-426d-949f-92e8be67da29", "sxw~0_yLYS6l1w~_ny5qf1Nr7-p2D4XGEE");
            var autopilotProfiles = graphClient.DeviceManagement.WindowsAutopilotDeploymentProfiles
                 .Request()
                 .Expand(d => new { d.Assignments, d.AssignedDevices })
                 .GetAsync()
               .GetAwaiter()
               .GetResult();

            var targetProfiles = autopilotProfiles.MapListV2<Microsoft.Graph.WindowsAutopilotDeploymentProfile, WindowsAutopilotDeploymentProfileYamlModel>();
           
        }
    }
}
