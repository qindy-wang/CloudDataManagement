using CloudDataManagement.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Imp
{
    public class IntuneDeleteService: IIntuneDeleteService
    {
        public async Task DeleteDeviceManagementScripts(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var scripts = await graphClient.DeviceManagement.DeviceManagementScripts.Request().GetAsync();
            Console.WriteLine($"Start to delete device management scripts, count: {scripts.Count}.");
            foreach (var script in scripts)
            {
                try
                {
                    Console.WriteLine($"  display name :{script.DisplayName}.");
                    await graphClient.DeviceManagement.DeviceManagementScripts[script.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skip to delete device management script, display name :{script.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete device management scripts finish.");
        }

        public async Task DeleteDeviceShellScripts(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var scripts = await graphClient.DeviceManagement.DeviceShellScripts.Request().GetAsync();
            Console.WriteLine($"Start to delete device management shell scripts, count: {scripts.Count}.");
            foreach (var script in scripts)
            {
                try
                {
                    Console.WriteLine($"  display name :{script.DisplayName}.");
                    await graphClient.DeviceManagement.DeviceShellScripts[script.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skip to delete device management shell script, display name :{script.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete device management shell scripts finish.");
        }

        public async Task DeleteEndpointDetections(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var intents = await graphClient.DeviceManagement.Intents.Request()
                .Filter("templateId eq 'e44c2ca3-2f9a-400a-a113-6cc88efd773d'")
                .GetAsync();
            Console.WriteLine($"Start to delete endpoint detection, count: {intents.Count}.");
            foreach (var intent in intents)
            {
                try
                {
                    Console.WriteLine($"  display name :{intent.DisplayName}.");
                    await graphClient.DeviceManagement.Intents[intent.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skip to delete endpoint detection, display name :{intent.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete endpoint detection finish.");
        }

        public async Task DeleteSecurityBaselines(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var intents = await graphClient.DeviceManagement.Intents
               .Request()
               .Filter("templateId ne 'e44c2ca3-2f9a-400a-a113-6cc88efd773d'")
               .GetAsync();
            Console.WriteLine($"Start to delete security baseline, count: {intents.Count}.");
            foreach (var intent in intents)
            {
                try
                {
                    Console.WriteLine($"  display name :{intent.DisplayName}.");
                    await graphClient.DeviceManagement.Intents[intent.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skip to delete security baseline, display name :{intent.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete security baseline finish.");
        }

        public async Task DeleteManagedApps(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);

            var configurations = await graphClient.DeviceAppManagement.TargetedManagedAppConfigurations
               .Request()
               .GetAsync();
            Console.WriteLine($"Start to delete App configuations(managed apps), count: {configurations.Count}.");
            foreach (var configuration in configurations)
            {
                try
                {
                    Console.WriteLine($"  display name :{configuration.DisplayName}.");
                    await graphClient.DeviceAppManagement.TargetedManagedAppConfigurations[configuration.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skipe to delete  App configuations(managed apps), display name :{configuration.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete  App configuations(managed apps) finish.");
        }

        public async Task DeleteManagedDevices(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);

            var configurations = await graphClient.DeviceAppManagement.MobileAppConfigurations
               .Request()
               .Filter("microsoft.graph.androidManagedStoreAppConfiguration/appSupportsOemConfig eq false or isof('microsoft.graph.androidManagedStoreAppConfiguration') eq false")
               .GetAsync();
            Console.WriteLine($"Start to delete App configuations(managed devices), count: {configurations.Count}.");
            foreach (var configuration in configurations)
            {
                try
                {
                    Console.WriteLine($"  display name :{configuration.DisplayName}.");
                    await graphClient.DeviceAppManagement.MobileAppConfigurations[configuration.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skipe to delete  App configuations(managed devices), display name :{configuration.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete  App configuations(managed devices) finish.");
        }

        public async Task DeleteAndroidApps(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);

            var apps = await graphClient.DeviceAppManagement.MobileApps
                .Request()
                .Filter("(microsoft.graph.managedApp/appAvailability eq null or microsoft.graph.managedApp/appAvailability eq 'lineOfBusiness' or isAssigned eq true)")
                .GetAsync();
            Console.WriteLine($"Start to delete mobile apps, count: {apps.Count}.");
            foreach (var app in apps)
            {
                try
                {
                    Console.WriteLine($"  display name :{app.DisplayName}.");
                    await graphClient.DeviceAppManagement.MobileApps[app.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skipe to delete mobile app, display name :{app.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete mobile app finish.");
        }

        public async Task DeleteCompliancePolicies(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var policies = await graphClient.DeviceManagement.DeviceCompliancePolicies.Request().GetAsync();
            Console.WriteLine($"Start to delete compliance policies, count: {policies.Count}.");
            foreach (var policy in policies)
            {
                try
                {
                    Console.WriteLine($"  display name :{policy.DisplayName}.");
                    await graphClient.DeviceManagement.DeviceCompliancePolicies[policy.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skipe to delete compliance policy, display name :{policy.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete compliance policy finish.");
        }

        public async Task DeleteAutopilotDeploymentProfiles(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);

            var profiles = await graphClient.DeviceManagement.WindowsAutopilotDeploymentProfiles
                .Request()
                .GetAsync();
            Console.WriteLine($"Start to delete autopilot deployment profiles, count: {profiles.Count}.");
            foreach (var profile in profiles)
            {
                try
                {
                    Console.WriteLine($"  display name :{profile.DisplayName}.");
                    await graphClient.DeviceManagement.WindowsAutopilotDeploymentProfiles[profile.Id].Request().DeleteAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skipe to delete autopilot deployment profile, display name :{profile.DisplayName}.");
                }
            }
            Console.WriteLine($"Delete autopilot deployment profiles finish.");
        }

        public async Task DeleteConfigurationPolicies(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var policies = await graphClient.DeviceManagement.ConfigurationPolicies.Request().GetAsync();
            Console.WriteLine($"Start to delete device configuation policy, count: {policies.Count}.");
            foreach (var policy in policies)
            {
                try
                {
                    Console.WriteLine($"  display name :{policy.Name}.");
                    await graphClient.DeviceManagement.ConfigurationPolicies[policy.Id].Request().GetAsync();
                }
                catch (Exception)
                {
                    Console.WriteLine($"  Skipe to delete device configuation policy, display name :{policy.Name}.");
                }
            }
            Console.WriteLine($"Delete device configuation policy finish.");
        }
    }
}
