using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostbuilder = CreateHostBuilder(args).Build();

            using( var env = hostbuilder.Services.CreateScope())
            {
                var services = env.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<Users>>();
                    var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var dbcontext = services.GetRequiredService<OnlineCoursesContext>();
                    dbcontext.Database.EnsureCreated();
                    SeedDB.InsertData(dbcontext, userManager, rolManager).Wait();
                }
                catch (Exception ex)
                {
                    var log = services.GetRequiredService<ILogger<Program>>();
                    log.LogError(ex, "error on db  migration");
                }
            }

            hostbuilder.Run();  
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
