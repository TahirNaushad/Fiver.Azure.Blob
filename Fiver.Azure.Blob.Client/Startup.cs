using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Fiver.Azure.Blob.Client
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
           Configuration = configuration;
        }

        public static IConfiguration Configuration;

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
            IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }
    }
}
