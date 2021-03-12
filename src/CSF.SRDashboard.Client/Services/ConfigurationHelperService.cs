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
        /// <summary>
        /// Following method is used for inserting ClientId and SecientSecret values from Azure key-vault before configuring
        /// Azure Active Directory related service. When configuring Azure Active Directory related service, inside appsettings.json
        /// there has to be a section "AzureAd" with its related ClientId and ClientSecret
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>AzureAd section of the configuration after updating it</returns>
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
