using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpClient();
            
            // Note: The example assumes that you will only use one key and passphrase to store tokens.
            // Of course there are ways to use more than one key/passphrase combination.
            // The best way is probably to store it in a database of some sort,
            // and take the relevant key/passphrase as arguments on each method in the service.
            // Another way could be to create a separate PCiVaultService singleton for each key/passphrase.
            // Your approach would depend on the way in which you safely store your keys and passphrases.
            services.AddSingleton(x => new PciVaultService(new ApiConfig
            {
                Username = Configuration["PciVault:Username"],
                Password = Configuration["PciVault:Password"],
                Key = Configuration["PciVault:Key"],
                Passphrase = Configuration["PciVault:Passphrase"]
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}