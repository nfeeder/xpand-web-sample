using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XpandDEVWebCourse.Web.Areas.Identity.Data;
using XpandDEVWebCourse.Web.Data;

[assembly: HostingStartup(typeof(XpandDEVWebCourse.Web.Areas.Identity.IdentityHostingStartup))]
namespace XpandDEVWebCourse.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<XpandDEVWebCourseWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("XpandDEVWebCourseWebContextConnection")));

                services.AddDefaultIdentity<XpandDEVWebCourseWebUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                    .AddEntityFrameworkStores<XpandDEVWebCourseWebContext>();
            });
        }
    }
}