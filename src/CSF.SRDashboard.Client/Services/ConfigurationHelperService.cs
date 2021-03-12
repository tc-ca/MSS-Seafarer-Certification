using CSF.Web.Client.Data.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public class ConfigurationHelperService
    {
        public static IConfigurationSection UpdateConfigurationForAzureAD(IConfiguration configuration )
        {
            AzureKeyVaultService keyVaultService = new AzureKeyVaultService(configuration);

            string azureAppRegistrationClientID = keyVaultService.GetSecretByName("AzureAD-ClientId");
            string azureAppRegistrationClientSecret = keyVaultService.GetSecretByName("AzureAD-ClientSecret");

            var azureADsection = configuration.GetSection("AzureAd");
            azureADsection.GetSection("ClientId").Value = azureAppRegistrationClientID;
            azureADsection.GetSection("ClientSecret").Value = azureAppRegistrationClientSecret;

            return azureADsection;
        }
    }
}
