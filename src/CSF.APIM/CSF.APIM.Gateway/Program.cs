using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var hostingEnvironment = hostingContext.HostingEnvironment;
                        if (hostingEnvironment.EnvironmentName == "dev")
                        {
                            var appAssembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                            if (appAssembly != null)
                            {
                                config.AddUserSecrets(appAssembly, optional: true);
                            }
                        }
                        config.AddJsonFile($"Ocelot.{hostingEnvironment.EnvironmentName}.json");
                    }
                    );
                }).ConfigureLogging(logging => logging.AddConsole());
    }
}
