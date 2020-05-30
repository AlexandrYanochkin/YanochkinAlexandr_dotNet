using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.Factory;

namespace SIC.Labs.Third
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<DAO>(DAOFactory.GetFactory(TypeOfFactory.MSSQL));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseMvc(route => {
                route.MapRoute(
                    name: "default",
                    template: @"{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
