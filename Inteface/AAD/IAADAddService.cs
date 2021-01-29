using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Inteface.AAD
{
    public interface IAADAddService
    {
        Task<User> AddUser(string clientId, string tenantId, string clientSecret, User user);
    }
}
