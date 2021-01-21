﻿using CloudDataManagement.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement
{
    public class IntuneDataDeleteService : IDeleteService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IIntuneDeleteService _intuneDeleteService;

        public IntuneDataDeleteService(IConfigurationRoot configuration, IIntuneDeleteService intuneDeleteService)
        {
            this._configuration = configuration;
            this._intuneDeleteService = intuneDeleteService;
        }

        public async Task Delete()
        {
            var clientId = _configuration.GetValue<string>("ClientId");
            var tenantId = _configuration.GetValue<string>("TenantId");
            var clientSecret = _configuration.GetValue<string>("ClientSecret");
            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(tenantId)
                && !string.IsNullOrEmpty(clientSecret))
            {
                await _intuneDeleteService.DeleteDeviceManagementScripts(clientId, tenantId, clientSecret);
                await _intuneDeleteService.DeleteDeviceShellScripts(clientId, tenantId, clientSecret);
                await _intuneDeleteService.DeleteSecurityBaselines(clientId, tenantId, clientSecret);
                await _intuneDeleteService.DeleteEndpointDetections(clientId, tenantId, clientSecret);
                await _intuneDeleteService.DeleteManagedApps(clientId, tenantId, clientSecret);
                await _intuneDeleteService.DeleteManagedDevices(clientId, tenantId, clientSecret);
            }
            else {
                Console.WriteLine($"Invliad configuration information, client id: {clientId}, tenant id: {tenantId}, client secret: {clientSecret}");
            }
        }
    }
}
