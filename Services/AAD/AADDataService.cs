using CloudDataManagement.Inteface;
using CloudDataManagement.Inteface.AAD;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Services
{
    public class AADDataService : IAADService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IAADSelectService _aadSelectService;
        private readonly IAADUpdateService _aadUpdateService;
        private readonly IAADAddService _aadAddService;

        public AADDataService(IConfigurationRoot configuration, 
            IAADSelectService aadSelectService, IAADUpdateService aadUpdateService, IAADAddService aadAddService)
        {
            this._configuration = configuration;
            this._aadSelectService = aadSelectService;
            this._aadUpdateService = aadUpdateService;
            this._aadAddService = aadAddService;
        }

        public async Task Add()
        {
            var clientId = _configuration.GetValue<string>("ClientSourceId");
            var tenantId = _configuration.GetValue<string>("TenantSourceId");
            var clientSecret = _configuration.GetValue<string>("ClientSourceSecret");
            var user = await _aadSelectService.SelectUser(clientId, tenantId, clientSecret, "Jason");

            var clientDestId = _configuration.GetValue<string>("ClientId");
            var tenantDestId = _configuration.GetValue<string>("TenantId");
            var clientDestSecret = _configuration.GetValue<string>("ClientSecret");
            await _aadAddService.AddUser(clientDestId, tenantDestId, clientDestSecret, user);
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task Select()
        {
            throw new NotImplementedException();
        }

        public async Task Update()
        {
            var clientId = _configuration.GetValue<string>("ClientSourceId");
            var tenantId = _configuration.GetValue<string>("TenantSourceId");
            var clientSecret = _configuration.GetValue<string>("ClientSourceSecret");
            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(tenantId)
                && !string.IsNullOrEmpty(clientSecret))
            {
                var user = await _aadSelectService.SelectUser(clientId, tenantId, clientSecret, "jason");
                if (user != null)
                {
                    var clientDestId = _configuration.GetValue<string>("ClientId");
                    var tenantDestId = _configuration.GetValue<string>("TenantId");
                    var clientDestSecret = _configuration.GetValue<string>("ClientSecret");


                    var targetUser = await _aadSelectService.SelectUser(clientDestId, tenantDestId, clientDestSecret, "jason");

                    await _aadUpdateService.UpdateUser(clientDestId, tenantDestId, clientDestSecret, targetUser.Id, user);
                }
            }
        }
    }
}
