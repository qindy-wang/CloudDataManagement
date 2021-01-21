using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Interface
{
    public interface IIntuneDeleteService
    {
        Task DeleteDeviceManagementScripts(string clientId, string tenantId, string clientSecret);
        Task DeleteDeviceShellScripts(string clientId, string tenantId, string clientSecret);

        Task DeleteSecurityBaselines(string clientId, string tenantId, string clientSecret);
        Task DeleteEndpointDetections(string clientId, string tenantId, string clientSecret);

        //App Configuration policy
        Task DeleteManagedApps(string clientId, string tenantId, string clientSecret);
        Task DeleteManagedDevices(string clientId, string tenantId, string clientSecret);
    }
}
