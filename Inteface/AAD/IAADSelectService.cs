using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Inteface.AAD
{
    public interface IAADSelectService
    {
        Task<User> SelectUser(string clientId, string tenantId, string clientSecret, string displayName);

        Task<IEnumerable<User>> SelectUsers(string clientId, string tenantId, string clientSecret);
    }
}
