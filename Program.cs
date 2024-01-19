using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using mabuddy.Data;
using mabuddy.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace mabuddy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<ApplicationDbContext>();

                var um = service.GetRequiredService<UserManager<ApplicationUser>>();
                var rm = service.GetRequiredService<RoleManager<IdentityRole>>();

                // Get the environment so we can check if this is running in development or otherwise
                var environment = service.GetService<IHostingEnvironment>();

                DbInitializer.Initialize(context, um, rm, environment.IsDevelopment());
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}