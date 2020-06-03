using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.Factory;
using SIC.Labs.Third.Models;
using Microsoft.Extensions.Logging;

namespace SIC.Labs.Third
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<DAO>(provider => DAOFactory.GetFactory(TypeOfFactory.MSSQL));
            services.AddAutoMapper(typeof(MapperProfile));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(route => {
                route.MapRoute(
                    name: "default", 
                    template: @"{controller=Home}/{action=Index}/{id?}"
                 );
            });
        }
    }
}
