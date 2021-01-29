using CloudDataManagement.Inteface.AAD;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Imp
{
    public class AADSelectService : IAADSelectService
    {
        public async Task<User> SelectUser(string clientId, string tenantId, string clientSecret, string displayName)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var users = await graphClient.Users
                .Request()
                .Filter($"displayName eq '{displayName}'")
                .GetAsync();
            return users.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> SelectUsers(string clientId, string tenantId, string clientSecret)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);
            var users = await graphClient.Users.Request().GetAsync();
            return users;
        }
    }
}
