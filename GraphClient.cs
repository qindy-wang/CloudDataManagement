using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudDataManagement
{
    public class GraphClient
    {
        private static object obj = new object();
        private static GraphServiceClient graphClient;

        private GraphClient()
        {
            
        }

        public static GraphServiceClient GetInstance(string clientId, string tennatId, string clientSecret)
        {
            //if (graphClient == null)
            //{
            //    lock (obj)
            //    {
            //        if (graphClient == null)
            //        {
                        IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                            .Create(clientId)
                            .WithTenantId(tennatId)
                            .WithClientSecret(clientSecret)
                            .Build();

                        ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);

                        graphClient = new GraphServiceClient(authProvider);
            //        }
            //    }
            //}
            return graphClient;
        }
    }
}
