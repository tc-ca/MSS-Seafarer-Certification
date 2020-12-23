using Microsoft.Extensions.Configuration;

namespace CSF.Web.Client.Tests.Unit.TestHelpers
{
    internal static class ConfigurationHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}