using CloudDataManagement.Imp;
using CloudDataManagement.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CloudDataManagement
{
    public class Startup
    {
        public static ServiceCollection Run()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            var services = ConfigurationServices(configuration);
            return services;
        }

        private static ServiceCollection ConfigurationServices(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfigurationRoot>(configuration);
            services.AddTransient<IIntuneDeleteService, IntuneDeleteService>();
            services.AddTransient<IDeleteService, IntuneDataDeleteService>();
            return services;
        }
    }
}
