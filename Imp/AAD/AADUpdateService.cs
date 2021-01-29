using CloudDataManagement.Inteface;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement
{
    public class AADUpdateService : IAADUpdateService
    {
        public async Task<User> UpdateUser(string clientId, string tenantId, string clientSecret, string userId, User user)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);

            var domains = await graphClient.Domains.Request().GetAsync();
            var defaultDomain = domains.Where(d => d.IsDefault.Value).FirstOrDefault()?.Id;

            var updatedUser = await graphClient.Users[userId]
                .Request()
                .UpdateAsync(new User()
                {
                    UserPrincipalName = $"{user.MailNickname}@{defaultDomain}",
                    MailNickname = user.MailNickname,
                    Mail = user.Mail,
                    DisplayName = user.DisplayName,
                    GivenName = user.GivenName,
                    Surname = user.Surname,
                    JobTitle = user.JobTitle,
                    Department = user.Department,
                    CompanyName = user.CompanyName,
                    City = user.City,
                    Country = user.Country,
                    EmployeeId = user.EmployeeId,
                    UsageLocation = user.UsageLocation,
                    UserType = user.UserType,
                    StreetAddress = user.StreetAddress,
                    State = user.State,
                    OfficeLocation = user.OfficeLocation
                    });
            return updatedUser;
        }
    }
}
