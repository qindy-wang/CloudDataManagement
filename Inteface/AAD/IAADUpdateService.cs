using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Inteface
{
    public interface IAADUpdateService
    {
        Task<User> UpdateUser(string clientId, string tenantId, string clientSecret, string userId, User user);
    }
}
