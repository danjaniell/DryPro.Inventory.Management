using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using DryPro.Inventory.Management.UI.Models;
using Microsoft.Extensions.Options;
using DryPro.Inventory.Management.Infrastructure.Data;

namespace DryPro.Inventory.Management.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-PH");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Endpoints>(Configuration.GetSection(nameof(Endpoints)));
            services.AddSingleton<IEndpoint>(x => x.GetRequiredService<IOptions<Endpoints>>().Value);
            services.AddControllersWithViews();
            services.AddSingleton<ProductCreateViewModel>();
            services.AddSingleton<ProductDetailsViewModel>();
            services.AddSingleton<AuxItemCreateViewModel>();
            services.AddSingleton<InventoryDetailsViewModel>();
            services.AddSingleton<InventoryAddViewModel>();
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
