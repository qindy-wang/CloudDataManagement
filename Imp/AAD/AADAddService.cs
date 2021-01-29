using CloudDataManagement.Inteface.AAD;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Imp.AAD
{
    public class AADAddService : IAADAddService
    {
        public async Task<User> AddUser(string clientId, string tenantId, string clientSecret, User user)
        {
            var graphClient = GraphClient.GetInstance(clientId, tenantId, clientSecret);

            var domains = await graphClient.Domains.Request().GetAsync();
            var defaultDomain = domains.Where(d => d.IsDefault.Value).FirstOrDefault()?.Id;
            var userPrincipalName = $"{user.MailNickname}@{defaultDomain}";

            var newUser = await graphClient.Users.Request()
               .AddAsync(new User
               {
                   DisplayName = user.DisplayName,
                   GivenName = user.GivenName,
                   Surname = user.Surname,
                   MailNickname = user.MailNickname,
                   UserPrincipalName = userPrincipalName,
                   JobTitle = user.JobTitle,
                   Department = user.Department,
                   CompanyName = user.CompanyName,
                   AccountEnabled = user.AccountEnabled,
                   PasswordProfile = new PasswordProfile
                   {
                       ForceChangePasswordNextSignIn = true,
                       Password = "1qaz2wsxE"
                   },
                   UsageLocation = user.UsageLocation,
                   StreetAddress = user.StreetAddress,
                   State = user.State,
                   OfficeLocation = user.OfficeLocation,
                   Country = user.Country,
                   City = user.City,
                   PostalCode = user.PostalCode,
                   BusinessPhones = user.BusinessPhones,
                   MobilePhone = user.MobilePhone,
                });
            return newUser;
        }
    }
}
