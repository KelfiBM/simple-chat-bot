using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Simple.Chat.Bot.App.Areas.Identity.Data;

[assembly: HostingStartup(typeof(Simple.Chat.Bot.App.Areas.Identity.IdentityHostingStartup))]
namespace Simple.Chat.Bot.App.Areas.Identity
{
  public class IdentityHostingStartup : IHostingStartup
  {
    public void Configure(IWebHostBuilder builder)
    {
      builder.ConfigureServices((context, services) =>
      {
        services.AddDbContext<IdentityDataContext>(options =>
            options.UseSqlServer(
                context.Configuration.GetConnectionString("IdentityDataContextConnection")));

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<IdentityDataContext>();
      });
    }
  }
}