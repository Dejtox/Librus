using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WineApp.Areas.Identity.Data;

[assembly: HostingStartup(typeof(WineApp.Areas.Identity.IdentityHostingStartup))]
namespace WineApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WineAppIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WineAppIdentityDbContextConnection")));

                services.AddDefaultIdentity<WebApp1User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<WineAppIdentityDbContext>();
            });
        }
    }
}