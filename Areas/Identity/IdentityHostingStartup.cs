using System;
using ManageStudentsSystem.Areas.Identity.Data;
using ManageStudentsSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ManageStudentsSystem.Areas.Identity.IdentityHostingStartup))]
namespace ManageStudentsSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ManageStudentsSystemContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("ManageStudentsSystemContextConnection")));
                services.AddIdentity<StudentManager, IdentityRole>().AddEntityFrameworkStores<ManageStudentsSystemContext>();

            });
        }
    }
}