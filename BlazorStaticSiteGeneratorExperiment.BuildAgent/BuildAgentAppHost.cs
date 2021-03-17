using System.Net.Http;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace BlazorStaticSiteGeneratorExperiment.BuildAgent
{
    public class BuildAgentAppHost : WebApplicationFactory<BlazorStaticSiteGeneratorExperiment.WebServer.Program>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder();
            builder.UseEnvironment(Environments.Production);


            builder.ConfigureWebHost(
                webHostBuilder =>
                {
                    webHostBuilder.UseStaticWebAssets();
                    webHostBuilder.ConfigureServices(services =>
                    {
                        services.Remove(new ServiceDescriptor(typeof(HttpClient), typeof(HttpClient), ServiceLifetime.Singleton));
                        services.AddScoped(_ => CreateDefaultClient());
                    });
                });

            return builder;
        }
    }
}
