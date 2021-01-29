using CloudDataManagement.Imp;
using CloudDataManagement.Imp.AAD;
using CloudDataManagement.Inteface;
using CloudDataManagement.Inteface.AAD;
using CloudDataManagement.Interface;
using CloudDataManagement.Services;
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
            services.AddTransient<IIntuneService, IntuneDataService>();

            services.AddTransient<IAADSelectService, AADSelectService>();
            services.AddTransient<IAADUpdateService, AADUpdateService>();
            services.AddTransient<IAADAddService, AADAddService>();
            services.AddTransient<IAADService, AADDataService>();
            return services;
        }
    }
}
