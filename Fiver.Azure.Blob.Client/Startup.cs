using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Fiver.Azure.Blob.Client
{
    public class Startup
    {
        public Startup(
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration;

        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddScoped<IAzureBlobStorage>(factory =>
            {
                return new AzureBlobStorage(new AzureBlobSetings(
                    storageAccount: Configuration["Blob_StorageAccount"], 
                    storageKey: Configuration["Blob_StorageKey"], 
                    containerName: Configuration["Blob_ContainerName"]));
            });

            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }
    }
}
