using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CSF.APIM.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                    webBuilder.UseSerilog((_, config) =>
                     {
                         config
                             .MinimumLevel.Information()
                             .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                             .Enrich.FromLogContext()
                             .WriteTo.File(@"\\wwwfiles\csf-apigw\Logs\log.txt", rollingInterval: RollingInterval.Day);
                     });

                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var hostingEnvironment = hostingContext.HostingEnvironment;
                        config.AddJsonFile($"Ocelot.{hostingEnvironment.EnvironmentName}.json");
                    });
                });
    }
}
