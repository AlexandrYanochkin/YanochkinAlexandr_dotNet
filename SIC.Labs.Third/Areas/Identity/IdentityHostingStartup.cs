using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIC.Labs.Third.Data;
using ApplicationContext = SIC.Labs.Third.Data.ApplicationContext;

[assembly: HostingStartup(typeof(SIC.Labs.Third.Areas.Identity.IdentityHostingStartup))]
namespace SIC.Labs.Third.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<ApplicationContext>();
            });
        }
    }
}