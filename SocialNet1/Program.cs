using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                 webBuilder.UseStartup<Startup>()
            )
            .UseSerilog((host, log) => log
                   .ReadFrom.Configuration(host.Configuration)
                   .MinimumLevel.Information()
                   .Enrich.FromLogContext()
                    .WriteTo.File($"Logs/Info/Full-SocialNet[ {DateTime.Now:yyyy-MM-dd}].log", LogEventLevel.Information)
                    .WriteTo.File($"Logs/Error/SocialNet[ {DateTime.Now:yyyy-MM-dd}].log", LogEventLevel.Error)

            )
            ;
    }
}
