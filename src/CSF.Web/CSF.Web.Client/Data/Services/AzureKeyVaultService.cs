﻿namespace CSF.Web.Client.Data.Services
{
    using System.Collections.Generic;
    using Microsoft.Azure.KeyVault;
    using Microsoft.Azure.KeyVault.Models;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Rest.Azure;

    /// <summary>
    ///  Represents the Azure key vault service.
    /// </summary>
    public class AzureKeyVaultService : IKeyVaultService
    {
        private string Dns;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultService"/> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public AzureKeyVaultService(IConfiguration configuration)
        {
            this.Dns = configuration.GetSection("AzureKeyVaultSettings")["KeyVaultServiceEndpoint"];
        }

        /// <summary>
        /// Gets a secret from azure key vault.
        /// </summary>
        /// <param name="secretName">Secret to get.</param>
        /// <returns>Secret value.</returns>
        public string GetSecretByName(string secretName)
        {
            using (var keyVaultClient = GetKeyVaultClient())
            {
                var sercret = keyVaultClient.GetSecretAsync(vaultBaseUrl: this.Dns, secretName: secretName).GetAwaiter().GetResult();

                return sercret.Value;
            }
        }

        /// <summary>
        /// Get a list of secrets from the azure key vault.
        /// </summary>
        /// <returns>List of secrets.</returns>
        public IEnumerable<SecretItem> GetListOfSecrets()
        {
            IEnumerable<SecretItem> secrets = new Page<SecretItem>();

            using (var keyVaultClient = GetKeyVaultClient())
            {
                secrets = keyVaultClient.GetSecretsAsync(vaultBaseUrl: this.Dns).GetAwaiter().GetResult();
            }

            return secrets;
        }

        /// <summary>
        /// Get the key vault client.
        /// </summary>
        /// <returns>Key vault client.</returns>
        private static KeyVaultClient GetKeyVaultClient()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                        new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            return keyVaultClient;
        }
    }
}
