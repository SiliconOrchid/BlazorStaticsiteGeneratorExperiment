using System;
using System.Linq;
using System.Net.Http;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using BlazorStaticSiteGeneratorExperiment.BlazorApp.Extensions;

namespace BlazorStaticSiteGeneratorExperiment.WebServer
{
    public class Startup
    {
        // Note:  We're injecting the blazor app as a service
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddContentfulGraphQLClient();
            services.AddSingleton<HttpClient>(sp =>
            {
                // Get the address that the blazorapp is currently running at
                var server = sp.GetRequiredService<IServer>();
                var addressFeature = server.Features.Get<IServerAddressesFeature>();
                string baseAddress = addressFeature.Addresses.First();
                return new HttpClient { BaseAddress = new Uri(baseAddress) };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseStatusCodePages();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
